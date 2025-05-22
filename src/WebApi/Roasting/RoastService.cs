using VictorFrye.MockingMirror.WebApi.ChatCompletion;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Roasting;

public class RoastService(IChatService chatService) : IRoastService
{
    public async Task<Roast> AddRoast(Roast roast, CancellationToken cancellationToken)
    {
        roast.CompletionText = await chatService.GetCompletion(roast.ImageBytes, roast.ImageMime, cancellationToken);

        if (roast.IncludeSpeech)
        {
            // roast.SpeechBytes = await speechService.GetSpeech(roast.CompletionText);
        }

        return roast;
    }
}
