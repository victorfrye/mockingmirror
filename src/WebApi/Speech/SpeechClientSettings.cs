namespace VictorFrye.MockingMirror.WebApi.Speech;

/// <summary>
/// The settings relevant to accessing the speech client.
/// /// </summary>
public sealed class SpeechClientSettings
{
    internal const string ConfigurationSectionName = nameof(SpeechClientSettings);

    /// <summary>
    /// Gets or sets the key used to authenticate to the speech client endpoint.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the region of the speech client.
    /// </summary>
    public string? Region { get; set; }
}
