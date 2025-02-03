using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;

namespace VictorFrye.MockingMirror.WebApi.Speech;

internal class SpeechService(IOptions<SpeechServiceOptions> options) : ISpeechService
{
    private readonly SpeechServiceOptions _options = options.Value;

    private SpeechConfig Config => SpeechConfig.FromSubscription(_options.ApiKey, _options.Region);

    public async Task<byte[]> GetSpeech(string text)
    {
        var config = Config;

        config.SpeechSynthesisLanguage = "en-US";
        config.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";
        config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm);

        using SpeechSynthesizer synthesizer = new(config, null);

        var result = await synthesizer.SpeakTextAsync(text);

        return result.AudioData;
    }
}
