namespace VictorFrye.MockingMirror.WebApi.Chat;

public interface IChatService
{
    Task<string> GetCompletion(IEnumerable<byte> imageBytes, string imageMime, CancellationToken cancellationToken = default);
}
