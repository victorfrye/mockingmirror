namespace VictorFrye.MockingMirror.WebApi.Roasting;

/// <summary>
/// Represents a generated roast response.
/// </summary>
public record RoastResponse
{
    /// <summary>
    /// The unique <see cref="Guid"/> identifier for the roast.
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// The generated roast completion text by the downstream chat client.
    /// </summary>
    public string CompletionText { get; set; } = string.Empty;

    /// <summary>
    /// The byte data of the generated text-to-speech audio for the roast completion.
    /// </summary>
    public byte[]? SpeechBytes { get; set; } = null;
}
