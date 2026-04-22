using System.Text.Json;
using System.Text.RegularExpressions;

namespace ObscuraProfileManager;

internal sealed class ProfileStore
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = true,
    };

    private static readonly Regex ProfileIdPattern = new(@"^acc_(\d{3})$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly RepositoryContext _repository;

    public ProfileStore(RepositoryContext repository)
    {
        _repository = repository;
        Directory.CreateDirectory(_repository.ConfigsDirectory);
        Directory.CreateDirectory(_repository.ProfilesDirectory);
    }

    public List<ProfileDocument> LoadProfiles()
    {
        var documents = new List<ProfileDocument>();

        foreach (var configPath in Directory.GetFiles(_repository.ConfigsDirectory, "*.json").OrderBy(path => path))
        {
            var json = File.ReadAllText(configPath);
            var config = JsonSerializer.Deserialize<ProfileConfig>(json, JsonOptions)
                ?? throw new InvalidDataException($"Could not parse profile config: {configPath}");

            if (string.IsNullOrWhiteSpace(config.Id))
            {
                config.Id = Path.GetFileNameWithoutExtension(configPath);
            }

            if (string.IsNullOrWhiteSpace(config.UserDataDir))
            {
                config.UserDataDir = $"../profiles/{config.Id}";
            }

            if (config.Args.Count == 0)
            {
                config.Args.Add("--start-maximized");
            }

            documents.Add(new ProfileDocument
            {
                ConfigPath = configPath,
                Config = config,
            });
        }

        return documents.OrderBy(document => document.Config.Id, StringComparer.OrdinalIgnoreCase).ToList();
    }

    public ProfileDocument CreateNextProfile()
    {
        var nextId = GetNextProfileId();
        var config = ProfileConfig.CreateDefault(nextId);
        var configPath = Path.Combine(_repository.ConfigsDirectory, $"{nextId}.json");
        var profileDirectory = Path.Combine(_repository.ProfilesDirectory, nextId);

        Directory.CreateDirectory(profileDirectory);
        SaveConfig(configPath, config);

        return new ProfileDocument
        {
            ConfigPath = configPath,
            Config = config,
        };
    }

    public void SaveProfile(ProfileDocument document, ProfileConfig config)
    {
        SaveConfig(document.ConfigPath, config);
        document.Config = config;
    }

    public string ResolveProfileDirectory(ProfileConfig config)
    {
        if (Path.IsPathRooted(config.UserDataDir))
        {
            return config.UserDataDir;
        }

        var configDirectory = Path.GetDirectoryName(Path.Combine(_repository.ConfigsDirectory, $"{config.Id}.json"))
            ?? _repository.ConfigsDirectory;

        return Path.GetFullPath(Path.Combine(configDirectory, config.UserDataDir));
    }

    private void SaveConfig(string configPath, ProfileConfig config)
    {
        var json = JsonSerializer.Serialize(config, JsonOptions);
        File.WriteAllText(configPath, json);
    }

    private string GetNextProfileId()
    {
        var maxIndex = 0;

        foreach (var filePath in Directory.GetFiles(_repository.ConfigsDirectory, "*.json"))
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var match = ProfileIdPattern.Match(fileName);
            if (!match.Success)
            {
                continue;
            }

            if (int.TryParse(match.Groups[1].Value, out var index))
            {
                maxIndex = Math.Max(maxIndex, index);
            }
        }

        return $"acc_{maxIndex + 1:000}";
    }
}
