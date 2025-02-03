namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal interface IOpenAIService
{
    Task<string> GetCompletion(string prompt, byte[] imageBytes, string imageMime, CancellationToken cancellationToken = default);
}
