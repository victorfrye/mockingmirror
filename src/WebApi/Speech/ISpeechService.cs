namespace VictorFrye.MockingMirror.WebApi.Speech;

public interface ISpeechService
{
    Task<byte[]> GetSpeech(string text);
}
