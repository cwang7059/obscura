const { Connection } = require('puppeteer-core/lib/cjs/puppeteer/common/Connection.js');
const { NodeWebSocketTransport } = require('puppeteer-core/lib/cjs/puppeteer/common/NodeWebSocketTransport.js');

const wsEndpoint =
  process.env.OBSCURA_WS || 'ws://127.0.0.1:9222/devtools/browser';
const targetUrl = process.argv[2] || 'https://example.com';
const settleMs = Number(process.env.OBSCURA_SETTLE_MS || '3000');

function wait(ms) {
  return new Promise(resolve => setTimeout(resolve, ms));
}

async function waitForSession(connection, sessionId, timeoutMs = 5000) {
  const start = Date.now();
  while (Date.now() - start < timeoutMs) {
    const session = connection.session(sessionId);
    if (session) {
      return session;
    }
    await wait(100);
  }
  throw new Error(`Timed out waiting for CDP session ${sessionId}.`);
}

async function sendWithRetry(session, method, params, attempts = 3) {
  let lastError = null;

  for (let attempt = 1; attempt <= attempts; attempt += 1) {
    try {
      return await session.send(method, params);
    } catch (error) {
      lastError = error;
      if (attempt === attempts) {
        break;
      }
      console.log(`[retry] ${method} attempt ${attempt} failed: ${error.message}`);
      await wait(500);
    }
  }

  throw lastError;
}

async function main() {
  const transport = await NodeWebSocketTransport.create(wsEndpoint);
  const connection = new Connection(wsEndpoint, transport, 0, 30000);

  try {
    console.log(`[connect] ${wsEndpoint}`);

    const { targetId } = await connection.send('Target.createTarget', {
      url: 'about:blank',
    });
    console.log(`[target] ${targetId}`);

    const { sessionId } = await connection.send('Target.attachToTarget', {
      targetId,
      flatten: true,
    });

    const session = await waitForSession(connection, sessionId);

    session.on('Page.domContentEventFired', () => {
      console.log('[event] domcontentloaded');
    });

    session.on('Page.loadEventFired', () => {
      console.log('[event] load');
    });

    await wait(300);

    await sendWithRetry(session, 'Page.enable');
    await sendWithRetry(session, 'Runtime.enable');
    await sendWithRetry(session, 'Page.navigate', { url: targetUrl });

    await wait(settleMs);

    const { result } = await sendWithRetry(session, 'Runtime.evaluate', {
      expression: `
        JSON.stringify({
          url: location.href,
          title: document.title,
          h1: document.querySelector('h1')?.innerText || null,
          htmlLength: document.documentElement?.outerHTML?.length || 0
        })
      `,
      returnByValue: true,
    });

    console.log(result.value);
  } finally {
    try {
      connection.dispose();
    } catch {
      // Ignore cleanup errors.
    }
  }
}

main().catch(error => {
  console.error('[error]', error && error.stack ? error.stack : error);
  process.exit(1);
});
