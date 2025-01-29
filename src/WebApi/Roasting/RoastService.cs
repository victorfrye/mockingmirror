using VictorFrye.MockingMirror.WebApi.OpenAI;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal class RoastService(IOpenAIService openAIService) : IRoastService
{
    private const string prompt =
    """
        You are a sentient mirror named "Mocking Mirror" that interacts with users who stand in front of you.

        The user will provide you with a picture of themselves and ask you to roast them.
        Here is the picture they provided:
        """;

    private readonly IOpenAIService _openAIService = openAIService;

    public async Task<RoastResponse> AddRoast(RoastRequest request)
    {
        // var completion = await _openAIService.GetCompletion(prompt, request.ImageBytes);

        return new RoastResponse()
        {
            TextBody = "You are a clown!!! Hahahahaha.",
            Prompt = prompt,
        };
    }
}
