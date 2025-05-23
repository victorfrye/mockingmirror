namespace VictorFrye.MockingMirror.WebApi.Roasting;

/// <summary>
/// Represents a request to roast an image.
/// </summary>
public record RoastRequest
{
    /// <summary>
    /// The byte data of the captured image to roast.
    /// </summary>
    public required byte[] ImageBytes { get; set; }

    /// <summary>
    /// The MIME type of the captured image. The default is "image/png".
    /// </summary>
    public string ImageMime { get; set; } = "image/png";

    /// <summary>
    /// A boolean indicator of whether to also generate speech from the roast test completion. The default is <see cref="true"/>.
    /// </summary>
    /// <remarks>
    /// This flag is commonly used to control flow while demonstrating the application.
    /// </remarks>
    public bool IncludeSpeech { get; set; } = true;
}
