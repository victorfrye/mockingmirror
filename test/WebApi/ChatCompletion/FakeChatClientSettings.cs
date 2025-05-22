using VictorFrye.MockingMirror.WebApi.ChatCompletion;

namespace VictorFrye.MockingMirror.WebApi.Tests.ChatCompletion;

internal class FakeChatClientSettings
{
    public static ChatClientSettings Build() => new Faker<ChatClientSettings>()
            .StrictMode(true)
            .RuleFor(o => o.Endpoint, f => f.Internet.Url())
            .RuleFor(o => o.ApiKey, f => f.Internet.Password())
            .RuleFor(o => o.Model, f => f.Lorem.Slug())
            .RuleFor(o => o.Provider, f => f.PickRandom<ChatProvider>())
            .Generate();
}
