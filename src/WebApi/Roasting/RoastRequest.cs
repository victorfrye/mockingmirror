namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastRequest
{
    public required byte[] ImageBytes { get; set; }

    public string ImageMime { get; set; } = "image/png";

    public bool IncludeSpeech { get; set; } = true;
}
