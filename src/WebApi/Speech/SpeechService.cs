using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;

namespace VictorFrye.MockingMirror.WebApi.Speech;

public class SpeechService(IOptionsSnapshot<SpeechClientSettings> options) : ISpeechService
{
    private SpeechConfig Config => SpeechConfig.FromSubscription(options.Value.ApiKey, options.Value.Region);

    private const string Language = "en-US";
    private const string VoiceName = "en-US-AvaMultilingualNeural";
    private readonly SpeechSynthesisOutputFormat OutputFormat = SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm;

    public async Task<byte[]> GetSpeech(string text)
    {
        var config = Config;

        config.SpeechSynthesisLanguage = Language;
        config.SpeechSynthesisVoiceName = VoiceName;
        config.SetSpeechSynthesisOutputFormat(OutputFormat);

        using SpeechSynthesizer synthesizer = new(config, null);

        var result = await synthesizer.SpeakTextAsync(text);

        return result.AudioData;
    }
}
