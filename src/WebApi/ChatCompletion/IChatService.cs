namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

public interface IChatService
{
    Task<string> GetCompletion(byte[] imageBytes, string imageMime, CancellationToken cancellationToken = default);
}
