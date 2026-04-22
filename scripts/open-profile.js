const fs = require('fs');
const path = require('path');
const { chromium } = require('playwright-core');

const repoRoot = path.resolve(__dirname, '..');
const defaultConfigPath = path.join(repoRoot, 'configs', 'acc_001.json');

const browserCandidates = {
  chrome: [
    path.join(process.env.LOCALAPPDATA || '', 'Google', 'Chrome', 'Application', 'chrome.exe'),
    path.join(process.env.PROGRAMFILES || '', 'Google', 'Chrome', 'Application', 'chrome.exe'),
    path.join(process.env['PROGRAMFILES(X86)'] || '', 'Google', 'Chrome', 'Application', 'chrome.exe'),
  ],
  edge: [
    path.join(process.env.LOCALAPPDATA || '', 'Microsoft', 'Edge', 'Application', 'msedge.exe'),
    path.join(process.env.PROGRAMFILES || '', 'Microsoft', 'Edge', 'Application', 'msedge.exe'),
    path.join(process.env['PROGRAMFILES(X86)'] || '', 'Microsoft', 'Edge', 'Application', 'msedge.exe'),
  ],
  brave: [
    path.join(process.env.LOCALAPPDATA || '', 'BraveSoftware', 'Brave-Browser', 'Application', 'brave.exe'),
    path.join(process.env.PROGRAMFILES || '', 'BraveSoftware', 'Brave-Browser', 'Application', 'brave.exe'),
    path.join(process.env['PROGRAMFILES(X86)'] || '', 'BraveSoftware', 'Brave-Browser', 'Application', 'brave.exe'),
  ],
};

function resolveMaybeRelative(baseDir, value) {
  if (!value) {
    return value;
  }

  if (path.isAbsolute(value)) {
    return value;
  }

  return path.resolve(baseDir, value);
}

function loadConfig(configPath) {
  const raw = fs.readFileSync(configPath, 'utf8');
  const config = JSON.parse(raw);
  const configDir = path.dirname(configPath);

  return {
    ...config,
    browser: (config.browser || 'chrome').toLowerCase(),
    userDataDir: resolveMaybeRelative(configDir, config.userDataDir || `../profiles/${config.id || 'acc_001'}`),
    executablePath: resolveMaybeRelative(configDir, config.executablePath || ''),
  };
}

function resolveBrowserExecutable(config) {
  if (config.executablePath) {
    if (!fs.existsSync(config.executablePath)) {
      throw new Error(`Configured browser executable was not found: ${config.executablePath}`);
    }
    return config.executablePath;
  }

  const candidates = browserCandidates[config.browser] || browserCandidates.chrome;
  const match = candidates.find(candidate => candidate && fs.existsSync(candidate));

  if (!match) {
    throw new Error(
      `No local ${config.browser} executable was found. ` +
        'Please install the browser or set "executablePath" in the config.'
    );
  }

  return match;
}

function toProxySetting(value) {
  if (!value || !String(value).trim()) {
    return undefined;
  }

  return { server: String(value).trim() };
}

function waitForExitSignal(context) {
  const browser = context.browser();
  if (!browser) {
    return Promise.resolve();
  }

  return new Promise(resolve => {
    let finished = false;

    const finish = async () => {
      if (finished) {
        return;
      }
      finished = true;
      process.off('SIGINT', onSigint);
      process.off('SIGTERM', onSigterm);
      resolve();
    };

    const onSigint = async () => {
      try {
        await context.close();
      } catch {
        // Ignore shutdown errors.
      }
      await finish();
    };

    const onSigterm = async () => {
      try {
        await context.close();
      } catch {
        // Ignore shutdown errors.
      }
      await finish();
    };

    process.on('SIGINT', onSigint);
    process.on('SIGTERM', onSigterm);
    browser.on('disconnected', finish);
  });
}

async function main() {
  const configArg = process.argv[2]
    ? path.resolve(process.cwd(), process.argv[2])
    : defaultConfigPath;
  const config = loadConfig(configArg);
  const executablePath = resolveBrowserExecutable(config);
  const autoCloseMs = Number(process.env.PW_PROFILE_AUTO_CLOSE_MS || config.autoCloseMs || 0);

  fs.mkdirSync(config.userDataDir, { recursive: true });

  console.log(`[profile] ${config.id || path.basename(config.userDataDir)}`);
  console.log(`[config] ${configArg}`);
  console.log(`[browser] ${executablePath}`);
  console.log(`[userDataDir] ${config.userDataDir}`);

  const context = await chromium.launchPersistentContext(config.userDataDir, {
    executablePath,
    headless: Boolean(config.headless),
    viewport: null,
    proxy: toProxySetting(config.proxy),
    args: Array.isArray(config.args) ? config.args : ['--start-maximized'],
  });

  const page = context.pages()[0] || (await context.newPage());
  const startUrl = config.startUrl || 'https://www.google.com';

  await page.goto(startUrl, { waitUntil: 'domcontentloaded' });
  console.log(`[opened] ${startUrl}`);

  if (autoCloseMs > 0) {
    console.log(`[auto-close] ${autoCloseMs}ms`);
    await new Promise(resolve => setTimeout(resolve, autoCloseMs));
    await context.close();
    return;
  }

  console.log('Browser profile is ready. After the first login, reopening the same config will reuse that account state.');
  console.log('Close the browser window, or press Ctrl+C in this console, to stop the launcher.');

  await waitForExitSignal(context);
}

main().catch(error => {
  console.error('[error]', error && error.stack ? error.stack : error);
  process.exit(1);
});
