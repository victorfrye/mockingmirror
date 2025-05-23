using VictorFrye.MockingMirror.WebApi.ChatCompletion;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

/// <summary>
/// Represents a service for generating and processing roasts using AI.
/// </summary>
public interface IRoastService
{
    Task<RoastResponse> AddRoast(RoastRequest roast, CancellationToken cancellationToken);
}

/// <summary>
/// The <see cref="RoastService"/> implementation that uses a chat service to generate roast text and a speech service to convert the text to speech.
/// /// </summary>
/// /// <param name="chatService">The generative AI <see cref="IChatService"/> to generate roast text.</param>
/// /// <param name="speechService">The text-to-speech <see cref="ISpeechService"/> to synthesize speech from the roast text.</param>
public class RoastService(IChatService chatService, ISpeechService speechService) : IRoastService
{
    /// <summary>
    /// Generates a roast from provided <see cref="RoastRequest"/> image data.
    /// </summary>
    /// <param name="request">The <see cref="RoastRequest"/> containing the image data to roast.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A <see cref="RoastResponse"/> containing the roast completion text and optional speech byte data.</returns>
    public async Task<RoastResponse> AddRoast(RoastRequest request, CancellationToken cancellationToken)
    {
        RoastResponse response = new()
        {
            CompletionText = await chatService.GetCompletion(request.ImageBytes, request.ImageMime, cancellationToken)
        };

        if (request.IncludeSpeech)
        {
            response.SpeechBytes = await speechService.GetSpeech(response.CompletionText);
        }

        return response;
    }
}
