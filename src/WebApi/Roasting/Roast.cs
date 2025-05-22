namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class Roast
{
    // MARK: Request
    public Guid Id { get; init; } = Guid.NewGuid();
    public required byte[] ImageBytes { get; set; }
    public string ImageMime { get; set; } = "image/png";
    public bool IncludeSpeech { get; set; } = false;

    // MARK: Response
    public string? Prompt { get; set; } = null;
    public string CompletionText { get; set; } = string.Empty;
    public byte[] SpeechBytes { get; set; } = [];
}
