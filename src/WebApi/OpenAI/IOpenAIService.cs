namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal interface IOpenAIService
{
    Task<string> GetCompletion(byte[] imageBytes, string imageMime, CancellationToken cancellationToken = default);
}
