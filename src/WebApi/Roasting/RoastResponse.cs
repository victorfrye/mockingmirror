namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastResponse
{
    public Guid Id { get; init; } = Guid.CreateVersion7();

    public required string TextBody { get; set; }

    public string? AudioBytes { get; set; } = null;

    public string? Prompt { get; set; } = null;

    public bool IsSuccess { get; set; } = true;

}
