using VictorFrye.MockingMirror.WebApi.OpenAI;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal class RoastService(IOpenAIService openAIService, ISpeechService speechService) : IRoastService
{
    private const string prompt =
    """
    You are a sentient mirror that interacts with users who stand in front of you.

    The user will provide you with a picture of themselves and ask you to mock them in a friendly but humorous manner.

    It is very important that you do not mention people or faces being mysterious, blurred, classified, hidden, or pixelated.

    For example you should not say "The person's face is blurred out" or "The person's face is hidden" or refer to 'incognito mode' or mention 'invisible faces' or 'cloak of invisibility'.

    Keep your response to two or three sentences.

    Some example responses include:
    1. Like a steamroller flattened a cat that had already been left out on the roof too long.
    2. Well, with that white hat you really should be wearing something a bit more summery, don't you think?
    3. Those dark eyes are so noticeable your mom will be asking if you're tired.
    4. The 1980s called. They want their hair and shoulder pads back.

    Respond humorously to the picture of the user.

    Here is the picture they provided:
    """;

    private readonly IOpenAIService _openAIService = openAIService;
    private readonly ISpeechService _speechService = speechService;

    public async Task<RoastResponse> AddRoast(RoastRequest request, CancellationToken cancellationToken)
    {
        var completion = await _openAIService.GetCompletion(prompt, request.ImageBytes, cancellationToken: cancellationToken);

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
