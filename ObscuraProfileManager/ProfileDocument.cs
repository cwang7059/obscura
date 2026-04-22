namespace ObscuraProfileManager;

internal sealed class ProfileDocument
{
    public required string ConfigPath { get; init; }

    public required ProfileConfig Config { get; set; }

    public string DisplayName => Config.Id;

    public override string ToString()
    {
        return DisplayName;
    }
}
