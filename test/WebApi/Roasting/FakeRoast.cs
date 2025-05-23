using VictorFrye.MockingMirror.WebApi.Roasting;

namespace VictorFrye.MockingMirror.WebApi.Tests.Roasting;

internal class FakeRoast
{
    internal static RoastRequest BuildRequest(bool defaultMimeType = true, bool includeSpeech = false) => new Faker<RoastRequest>()
            .StrictMode(true)
            .RuleFor(x => x.ImageBytes, f => f.Random.Bytes(100))
            .RuleFor(x => x.ImageMime, (f, x) => defaultMimeType ? x.ImageMime : f.System.MimeType())
            .RuleFor(x => x.IncludeSpeech, includeSpeech)
            .Generate();

    internal static RoastResponse BuildResponse() => new Faker<RoastResponse>()
            .StrictMode(true)
            .RuleFor(x => x.Id, (f, x) => x.Id)
            .RuleFor(x => x.CompletionText, f => f.Lorem.Sentences(3))
            .RuleFor(x => x.SpeechBytes, f => f.Random.Bytes(100))
            .Generate();
}
