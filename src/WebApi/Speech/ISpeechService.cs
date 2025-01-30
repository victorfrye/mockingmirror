namespace VictorFrye.MockingMirror.WebApi.Speech;

internal interface ISpeechService
{
    Task<string> GetSpeech(string text);
}
