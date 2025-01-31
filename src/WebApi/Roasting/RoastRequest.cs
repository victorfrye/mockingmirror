namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastRequest
{
    public required string ImageBytes { get; set; }

    public bool IncludeSpeech { get; set; } = true;
}
