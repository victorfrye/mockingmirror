using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;

namespace VictorFrye.MockingMirror.WebApi.Speech;

internal class SpeechService(IOptions<SpeechServiceOptions> options) : ISpeechService
{
    private readonly SpeechServiceOptions _options = options.Value;

    private SpeechConfig Config => SpeechConfig.FromSubscription(_options.ApiKey, _options.Region);

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
