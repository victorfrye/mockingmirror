namespace VictorFrye.MockingMirror.WebApi.Speech;

public interface ISpeechService
{
    Task<IEnumerable<byte>> GetSpeech(string text);
}
