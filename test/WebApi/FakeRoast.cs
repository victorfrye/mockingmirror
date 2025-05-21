using VictorFrye.MockingMirror.WebApi.Roasting;

namespace VictorFrye.MockingMirror.WebApi.Tests;

internal class FakeRoast
{
    public static Roast Build(bool defaultMimeType = true, bool includeSpeech = false) => new Faker<Roast>()
            .StrictMode(true)
            .RuleFor(r => r.Id, (_, r) => r.Id)
            .RuleFor(r => r.ImageBytes, f => f.Random.Bytes(100))
            .RuleFor(r => r.ImageMime, (f, r) => defaultMimeType ? r.ImageMime : f.System.MimeType())
            .RuleFor(r => r.IncludeSpeech, includeSpeech)
            .RuleFor(r => r.Prompt, _ => null)
            .RuleFor(r => r.CompletionText, _ => string.Empty)
            .RuleFor(r => r.SpeechBytes, _ => [])
            .Generate();
}
