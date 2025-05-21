using Microsoft.Extensions.AI;

using VictorFrye.MockingMirror.WebApi.Chat;

namespace VictorFrye.MockingMirror.WebApi.Tests.Chat;

public class ChatClientFactoryTests
{
    private static ChatClientFactory Sut => new();

    [Theory]
    [InlineData(ChatClientKind.OpenAI, "OpenAIChatClient")]
    public void CreateReturnsExpectedClientType(ChatClientKind kind, string expectedType)
    {
        var expectedSettings = FakeChatClientSettings.Build();

        var client = Sut.Create(expectedSettings, kind);

        Assert.Equal(expectedType, client.GetType().Name);
        Assert.IsType<IChatClient>(client, exactMatch: false);
    }
}
