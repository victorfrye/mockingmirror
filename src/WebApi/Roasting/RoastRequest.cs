namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastRequest
{
    public required string ImageBytes { get; set; }

    public string? SpeechBytes { get; set; } = null;
}