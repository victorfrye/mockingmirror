namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal interface IOpenAIService
{
    Task<string> GetCompletion(string prompt, string imageBytes, string imageMime = "image/png", CancellationToken cancellationToken = default);
}
