using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

public class ChatClientSettings
{
    public const string ConfigurationSectionName = nameof(ChatClientSettings);

    [Url]
    public string? Endpoint { get; set; }

    public string? ApiKey { get; set; }

    public required string Model { get; set; }

    public required ChatProvider Provider { get; set; } = ChatProvider.Unknown;

    public static bool TryParse(string? connectionString, [NotNullWhen(true)] out ChatClientSettings? settings)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            settings = null;
            return false;
        }

        var span = connectionString.AsSpan();
        var settingsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var range in span.Split(';'))
        {
            var part = span[range];
            var index = part.IndexOf('=');

            if (index <= 0 || index == part.Length - 1)
            {
                continue;
            }

            var key = part[.. index].Trim();
            var value = part[(index + 1) ..].Trim();
            settingsDictionary[key.ToString()] = value.ToString();
        }

        settingsDictionary.TryGetValue(nameof(Endpoint), out var endpoint);
        settingsDictionary.TryGetValue(nameof(ApiKey), out var apiKey);

        if (!settingsDictionary.TryGetValue(nameof(Model), out var model)
            || !settingsDictionary.TryGetValue(nameof(Provider), out var provider))
        {
            settings = null;
            return false;
        }

        settings = new ChatClientSettings
        {
            Endpoint = endpoint,
            ApiKey = apiKey,
            Model = model,
            Provider = Enum.TryParse<ChatProvider>(provider, ignoreCase: true, out var parsedProvider) ? parsedProvider : ChatProvider.Unknown
        };
        return true;
    }
}
