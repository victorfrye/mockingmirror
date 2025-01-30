namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastResponse
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public required string CompletionText { get; set; }

    public string? SpeechBytes { get; set; } = null;

    public string? Prompt { get; set; } = null;
}
