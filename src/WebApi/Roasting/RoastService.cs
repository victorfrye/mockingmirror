using VictorFrye.MockingMirror.WebApi.OpenAI;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal class RoastService(IOpenAIService openAIService, ISpeechService speechService) : IRoastService
{
    private const string prompt =
    """
    You are a sentient mirror named "Mocking Mirror" that interacts with users who stand in front of you.

    The user will provide you with a picture of themselves and ask you to roast them.
    Here is the picture they provided:
    """;

    private readonly IOpenAIService _openAIService = openAIService;
    private readonly ISpeechService _speechService = speechService;

    public async Task<RoastResponse> AddRoast(RoastRequest request)
    {
        var completion = await _openAIService.GetCompletion(prompt, request.ImageBytes);

        string? speechBytes = null;

        if (request.IncludeSpeech)
        {
            speechBytes = await _speechService.GetSpeech(completion);
        }

        return new RoastResponse()
        {
            CompletionText = completion,
            SpeechBytes = speechBytes,
            Prompt = prompt,
        };
    }
}
