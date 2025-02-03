namespace VictorFrye.MockingMirror.WebApi.Speech;

internal interface ISpeechService
{
    Task<byte[]> GetSpeech(string text);
}
