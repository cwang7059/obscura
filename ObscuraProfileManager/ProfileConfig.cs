using System.Text.Json.Serialization;

namespace ObscuraProfileManager;

internal sealed class ProfileConfig
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("browser")]
    public string Browser { get; set; } = "chrome";

    [JsonPropertyName("userDataDir")]
    public string UserDataDir { get; set; } = string.Empty;

    [JsonPropertyName("startUrl")]
    public string StartUrl { get; set; } = "https://accounts.google.com";

    [JsonPropertyName("headless")]
    public bool Headless { get; set; }

    [JsonPropertyName("proxy")]
    public string Proxy { get; set; } = string.Empty;

    [JsonPropertyName("args")]
    public List<string> Args { get; set; } = ["--start-maximized"];

    [JsonPropertyName("executablePath")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ExecutablePath { get; set; }

    public static ProfileConfig CreateDefault(string id)
    {
        return new ProfileConfig
        {
            Id = id,
            Browser = "chrome",
            UserDataDir = $"../profiles/{id}",
            StartUrl = "https://accounts.google.com",
            Headless = false,
            Proxy = string.Empty,
            Args = ["--start-maximized"],
            ExecutablePath = null,
        };
    }
}
