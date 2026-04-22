using System.Diagnostics;

namespace ObscuraProfileManager;

public partial class Form1 : Form
{
    private readonly RepositoryContext _repository;
    private readonly ProfileStore _profileStore;
    private readonly ProfileLauncher _profileLauncher;
    private readonly Dictionary<string, Process> _runningLaunchers = new(StringComparer.OrdinalIgnoreCase);

    private List<ProfileDocument> _profiles = [];
    private ProfileDocument? _selectedProfile;

    public Form1()
    {
        _repository = RepositoryContext.Discover(AppContext.BaseDirectory);
        _profileStore = new ProfileStore(_repository);
        _profileLauncher = new ProfileLauncher(_repository);

        InitializeComponent();
        ConfigureUi();
        LoadProfiles(selectProfileId: null);
    }

    private void ConfigureUi()
    {
        cboBrowser.Items.AddRange(["chrome", "edge", "brave"]);
        lblRepoRoot.Text = _repository.RootPath;
        txtArgs.Text = "--start-maximized";

        lstProfiles.SelectedIndexChanged += LstProfiles_SelectedIndexChanged;
        btnCreateProfile.Click += BtnCreateProfile_Click;
        btnRefreshProfiles.Click += (_, _) => LoadProfiles(_selectedProfile?.Config.Id);
        btnOpenConfigsFolder.Click += (_, _) => OpenFolder(_repository.ConfigsDirectory);
        btnOpenProfileFolder.Click += BtnOpenProfileFolder_Click;
        btnSaveProfile.Click += BtnSaveProfile_Click;
        btnStartProfile.Click += BtnStartProfile_Click;
    }

    private void LoadProfiles(string? selectProfileId)
    {
        _profiles = _profileStore.LoadProfiles();

        if (_profiles.Count == 0)
        {
            var created = _profileStore.CreateNextProfile();
            AppendLog($"Created the first profile: {created.Config.Id}");
            _profiles = _profileStore.LoadProfiles();
            selectProfileId = created.Config.Id;
        }

        lstProfiles.BeginUpdate();
        try
        {
            lstProfiles.Items.Clear();
            foreach (var profile in _profiles)
            {
                lstProfiles.Items.Add(profile);
            }
        }
        finally
        {
            lstProfiles.EndUpdate();
        }

        var profileToSelect = _profiles.FirstOrDefault(profile =>
            string.Equals(profile.Config.Id, selectProfileId, StringComparison.OrdinalIgnoreCase))
            ?? _profiles.FirstOrDefault();

        if (profileToSelect is not null)
        {
            lstProfiles.SelectedItem = profileToSelect;
        }
    }

    private void BindProfile(ProfileDocument profile)
    {
        _selectedProfile = profile;

        txtProfileId.Text = profile.Config.Id;
        cboBrowser.SelectedItem = profile.Config.Browser;
        if (cboBrowser.SelectedIndex < 0)
        {
            cboBrowser.SelectedItem = "chrome";
        }

        txtStartUrl.Text = profile.Config.StartUrl;
        txtUserDataDir.Text = profile.Config.UserDataDir;
        txtProxy.Text = profile.Config.Proxy;
        txtExecutablePath.Text = profile.Config.ExecutablePath ?? string.Empty;
        txtConfigPath.Text = profile.ConfigPath;
        chkHeadless.Checked = profile.Config.Headless;
        txtArgs.Lines = profile.Config.Args.Count > 0
            ? profile.Config.Args.ToArray()
            : ["--start-maximized"];
    }

    private ProfileConfig ReadEditor()
    {
        if (_selectedProfile is null)
        {
            throw new InvalidOperationException("Please select a profile first.");
        }

        var browser = cboBrowser.SelectedItem?.ToString()?.Trim().ToLowerInvariant();
        if (string.IsNullOrWhiteSpace(browser))
        {
            browser = "chrome";
        }

        var startUrl = txtStartUrl.Text.Trim();
        if (string.IsNullOrWhiteSpace(startUrl))
        {
            startUrl = "https://accounts.google.com";
        }

        var userDataDir = txtUserDataDir.Text.Trim();
        if (string.IsNullOrWhiteSpace(userDataDir))
        {
            userDataDir = $"../profiles/{_selectedProfile.Config.Id}";
        }

        var args = txtArgs.Lines
            .Select(line => line.Trim())
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .ToList();

        if (args.Count == 0)
        {
            args.Add("--start-maximized");
        }

        return new ProfileConfig
        {
            Id = _selectedProfile.Config.Id,
            Browser = browser,
            StartUrl = startUrl,
            UserDataDir = userDataDir,
            Proxy = txtProxy.Text.Trim(),
            Headless = chkHeadless.Checked,
            ExecutablePath = string.IsNullOrWhiteSpace(txtExecutablePath.Text) ? null : txtExecutablePath.Text.Trim(),
            Args = args,
        };
    }

    private ProfileDocument SaveSelectedProfile()
    {
        if (_selectedProfile is null)
        {
            throw new InvalidOperationException("Please select a profile first.");
        }

        var config = ReadEditor();
        _profileStore.SaveProfile(_selectedProfile, config);
        Directory.CreateDirectory(_profileStore.ResolveProfileDirectory(config));
        AppendLog($"Saved profile {config.Id}.");

        LoadProfiles(config.Id);
        var refreshed = _profiles.First(profile => string.Equals(profile.Config.Id, config.Id, StringComparison.OrdinalIgnoreCase));
        _selectedProfile = refreshed;
        return refreshed;
    }

    private void AppendLog(string message)
    {
        if (IsDisposed || Disposing)
        {
            return;
        }

        if (InvokeRequired)
        {
            BeginInvoke(() => AppendLog(message));
            return;
        }

        var line = $"[{DateTime.Now:HH:mm:ss}] {message}";
        if (txtLog.TextLength == 0)
        {
            txtLog.Text = line;
        }
        else
        {
            txtLog.AppendText(Environment.NewLine + line);
        }

        txtLog.SelectionStart = txtLog.TextLength;
        txtLog.ScrollToCaret();
    }

    private void OpenFolder(string path)
    {
        Directory.CreateDirectory(path);
        Process.Start(new ProcessStartInfo
        {
            FileName = path,
            UseShellExecute = true,
        });
    }

    private void LstProfiles_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (lstProfiles.SelectedItem is ProfileDocument profile)
        {
            BindProfile(profile);
        }
    }

    private void BtnCreateProfile_Click(object? sender, EventArgs e)
    {
        try
        {
            var created = _profileStore.CreateNextProfile();
            AppendLog($"Created profile {created.Config.Id}.");
            LoadProfiles(created.Config.Id);
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void BtnOpenProfileFolder_Click(object? sender, EventArgs e)
    {
        try
        {
            if (_selectedProfile is null)
            {
                throw new InvalidOperationException("Please select a profile first.");
            }

            var config = ReadEditor();
            OpenFolder(_profileStore.ResolveProfileDirectory(config));
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private void BtnSaveProfile_Click(object? sender, EventArgs e)
    {
        try
        {
            SaveSelectedProfile();
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
    }

    private async void BtnStartProfile_Click(object? sender, EventArgs e)
    {
        if (_selectedProfile is null)
        {
            ShowError("Please select a profile first.");
            return;
        }

        try
        {
            btnStartProfile.Enabled = false;
            var profile = SaveSelectedProfile();

            if (_runningLaunchers.TryGetValue(profile.Config.Id, out var existingProcess) && !existingProcess.HasExited)
            {
                AppendLog($"Profile {profile.Config.Id} is already running.");
                return;
            }

            var process = await _profileLauncher.LaunchProfileAsync(profile, AppendLog, CancellationToken.None);
            _runningLaunchers[profile.Config.Id] = process;

            process.Exited += (_, _) =>
            {
                if (_runningLaunchers.TryGetValue(profile.Config.Id, out var trackedProcess) && trackedProcess == process)
                {
                    _runningLaunchers.Remove(profile.Config.Id);
                }

                AppendLog($"Launcher for {profile.Config.Id} exited with code {process.ExitCode}.");
            };
        }
        catch (Exception ex)
        {
            ShowError(ex.Message);
        }
        finally
        {
            btnStartProfile.Enabled = true;
        }
    }

    private void ShowError(string message)
    {
        AppendLog(message);
        MessageBox.Show(this, message, "Obscura Profile Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
