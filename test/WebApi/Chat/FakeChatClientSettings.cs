using VictorFrye.MockingMirror.WebApi.Chat;

namespace VictorFrye.MockingMirror.WebApi.Tests.Chat;

internal class FakeChatClientSettings
{
    public static ChatClientSettings Build() => new Faker<ChatClientSettings>()
            .StrictMode(true)
            .RuleFor(o => o.Endpoint, f => f.Internet.Url())
            .RuleFor(o => o.ApiKey, f => f.Internet.Password())
            .RuleFor(o => o.DeploymentName, f => f.Lorem.Slug())
            .Generate();
}
