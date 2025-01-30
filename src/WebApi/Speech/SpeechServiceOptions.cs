using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.Speech;

internal class SpeechServiceOptions
{
    public const string ConfigurationSectionName = nameof(SpeechServiceOptions);

    [Required]
    public required string ApiKey { get; set; }

    [Required]
    public required string Region { get; set; }
}
