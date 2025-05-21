using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.Speech;

public class SpeechClientSettings
{
    public const string ConfigurationSectionName = nameof(SpeechClientSettings);

    [Required]
    public required string ApiKey { get; set; }

    [Required]
    public required string Region { get; set; }
}
