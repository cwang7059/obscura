using System.ComponentModel;
using System.Diagnostics;

namespace ObscuraProfileManager;

internal sealed class ProfileLauncher
{
    private readonly RepositoryContext _repository;

    public ProfileLauncher(RepositoryContext repository)
    {
        _repository = repository;
    }

    public async Task EnsureDependenciesAsync(Action<string> log, CancellationToken cancellationToken)
    {
        if (File.Exists(_repository.PlaywrightPackagePath))
        {
            return;
        }

        log("playwright-core is not installed yet. Running npm install...");
        var exitCode = await RunForExitAsync(
            "npm.cmd",
            ["install"],
            null,
            log,
            cancellationToken
        );

        if (exitCode != 0)
        {
            throw new InvalidOperationException($"npm install failed with exit code {exitCode}.");
        }
    }

    public async Task<Process> LaunchProfileAsync(
        ProfileDocument profile,
        Action<string> log,
        CancellationToken cancellationToken,
        IReadOnlyDictionary<string, string?>? environmentOverrides = null)
    {
        await EnsureDependenciesAsync(log, cancellationToken);

        var startInfo = new ProcessStartInfo
        {
            FileName = "node",
            WorkingDirectory = _repository.RootPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        startInfo.ArgumentList.Add(_repository.OpenProfileScriptPath);
        startInfo.ArgumentList.Add(profile.ConfigPath);

        if (environmentOverrides is not null)
        {
            foreach (var pair in environmentOverrides)
            {
                if (pair.Value is null)
                {
                    startInfo.Environment.Remove(pair.Key);
                }
                else
                {
                    startInfo.Environment[pair.Key] = pair.Value;
                }
            }
        }

        var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true,
        };

        process.OutputDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                log(args.Data);
            }
        };

        process.ErrorDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                log($"ERROR: {args.Data}");
            }
        };

        try
        {
            if (!process.Start())
            {
                throw new InvalidOperationException("The profile launcher process could not be started.");
            }
        }
        catch (Win32Exception ex)
        {
            throw new InvalidOperationException(
                "Could not start Node.js. Make sure node is installed and available in PATH.",
                ex
            );
        }

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        log($"Launcher started for {profile.Config.Id}.");

        return process;
    }

    private async Task<int> RunForExitAsync(
        string fileName,
        IReadOnlyList<string> arguments,
        IReadOnlyDictionary<string, string?>? environmentOverrides,
        Action<string> log,
        CancellationToken cancellationToken)
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = fileName,
            WorkingDirectory = _repository.RootPath,
            UseShellExecute = false,
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };

        foreach (var argument in arguments)
        {
            startInfo.ArgumentList.Add(argument);
        }

        if (environmentOverrides is not null)
        {
            foreach (var pair in environmentOverrides)
            {
                if (pair.Value is null)
                {
                    startInfo.Environment.Remove(pair.Key);
                }
                else
                {
                    startInfo.Environment[pair.Key] = pair.Value;
                }
            }
        }

        using var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true,
        };

        var completionSource = new TaskCompletionSource<int>(TaskCreationOptions.RunContinuationsAsynchronously);

        process.OutputDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                log(args.Data);
            }
        };

        process.ErrorDataReceived += (_, args) =>
        {
            if (!string.IsNullOrWhiteSpace(args.Data))
            {
                log(args.Data);
            }
        };

        process.Exited += (_, _) => completionSource.TrySetResult(process.ExitCode);

        try
        {
            if (!process.Start())
            {
                throw new InvalidOperationException($"Could not start process: {fileName}");
            }
        }
        catch (Win32Exception ex)
        {
            throw new InvalidOperationException(
                $"Could not start {fileName}. Make sure it is installed and available in PATH.",
                ex
            );
        }

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        using var cancellationRegistration = cancellationToken.Register(() =>
        {
            try
            {
                if (!process.HasExited)
                {
                    process.Kill(entireProcessTree: true);
                }
            }
            catch
            {
                // Ignore cancellation cleanup errors.
            }
        });

        return await completionSource.Task.ConfigureAwait(false);
    }
}
