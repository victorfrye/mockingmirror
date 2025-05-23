using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Options;

namespace VictorFrye.MockingMirror.WebApi.Speech;

/// <summary>
/// Represents a service that provides AI text-to-speech functionality.
/// </summary>
public interface ISpeechService
{
    /// <summary>
    /// Get synthesized speech from the provided <paramref name="text"/>.
    /// </summary>
    /// <param name="text">The text to synthesize into speech.</param>
    /// <returns>A <see cref="byte[]"> containing the synthesized speech data.</returns>
    Task<byte[]> GetSpeech(string text);
}

/// <summary>
/// The <see cref="SpeechService"/> implementation that uses the Azure Cognitive Services Speech SDK to synthesize speech from text.
/// </summary>
/// <param name="options">The <see cref="IOptions{T}"/> containing the speech client settings.</param>
public class SpeechService(IOptions<SpeechClientSettings> options) : ISpeechService
{
    private SpeechConfig Config => SpeechConfig.FromSubscription(options.Value.ApiKey, options.Value.Region);

    private const string Language = "en-US";
    private const string VoiceName = "en-US-AvaMultilingualNeural";
    private readonly SpeechSynthesisOutputFormat OutputFormat = SpeechSynthesisOutputFormat.Riff16Khz16BitMonoPcm;

    /// <inheritdoc/>
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
