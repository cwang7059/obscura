namespace ObscuraProfileManager;

internal sealed class RepositoryContext
{
    public required string RootPath { get; init; }

    public string ConfigsDirectory => Path.Combine(RootPath, "configs");

    public string ProfilesDirectory => Path.Combine(RootPath, "profiles");

    public string ScriptsDirectory => Path.Combine(RootPath, "scripts");

    public string OpenProfileScriptPath => Path.Combine(ScriptsDirectory, "open-profile.js");

    public string PackageJsonPath => Path.Combine(RootPath, "package.json");

    public string PlaywrightPackagePath => Path.Combine(RootPath, "node_modules", "playwright-core", "package.json");

    public static RepositoryContext Discover(string startDirectory)
    {
        var current = new DirectoryInfo(Path.GetFullPath(startDirectory));

        while (current is not null)
        {
            var packageJson = Path.Combine(current.FullName, "package.json");
            var openProfileScript = Path.Combine(current.FullName, "scripts", "open-profile.js");

            if (File.Exists(packageJson) && File.Exists(openProfileScript))
            {
                return new RepositoryContext
                {
                    RootPath = current.FullName,
                };
            }

            current = current.Parent;
        }

        throw new DirectoryNotFoundException(
            "Could not locate the repository root. Make sure the app is started from inside the repo."
        );
    }
}
