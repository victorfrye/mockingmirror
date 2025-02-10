using VictorFrye.MockingMirror.WebApi.OpenAI;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal class RoastService(IOpenAIService openAIService, ISpeechService speechService) : IRoastService
{
    private readonly IOpenAIService _openAIService = openAIService;
    private readonly ISpeechService _speechService = speechService;

    public async Task<RoastResponse> AddRoast(RoastRequest request, CancellationToken cancellationToken)
    {
        var completion = await _openAIService.GetCompletion(request.ImageBytes, request.ImageMime, cancellationToken);

        byte[]? speechBytes = null;

        if (request.IncludeSpeech)
        {
            speechBytes = await _speechService.GetSpeech(completion);
        }

        return new RoastResponse()
        {
            CompletionText = completion,
            SpeechBytes = speechBytes,
        };
    }
}
