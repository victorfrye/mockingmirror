using VictorFrye.MockingMirror.WebApi.ChatCompletion;

namespace VictorFrye.MockingMirror.WebApi.Tests.ChatCompletion;

internal class FakeChatClientSettings
{
    internal static ChatClientSettings Build() => new Faker<ChatClientSettings>()
            .StrictMode(true)
            .RuleFor(x => x.Endpoint, f => new Uri(f.Internet.Url()))
            .RuleFor(x => x.ApiKey, f => f.Internet.Password())
            .RuleFor(x => x.DeploymentName, f => f.Lorem.Slug())
            .Generate();
}
