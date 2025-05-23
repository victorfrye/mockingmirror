using Microsoft.Extensions.AI;

using VictorFrye.MockingMirror.WebApi.ChatCompletion;

namespace VictorFrye.MockingMirror.WebApi.Tests.ChatCompletion;

public class OpenAIChatServiceTests
{
    private readonly Mock<IChatClient> _chatClient = new();

    private ChatService Sut => new(_chatClient.Object);

    [Fact]
    public async Task GetCompletionWithSingleMessageResponseReturnsString()
    {
        var expectedImageBytes = new Faker().Random.Bytes(100);
        const string expectedImageMime = "image/png";
        var expectedSettings = FakeChatClientSettings.Build();
        var expectedCompletion = new Faker().Lorem.Sentences();

        _chatClient.Setup(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new ChatResponse(
                   [
                       new ChatMessage(ChatRole.Assistant, expectedCompletion)
                   ]));

        var actualCompletion = await Sut.GetCompletion(expectedImageBytes, expectedImageMime, TestContext.Current.CancellationToken);

        _chatClient.Verify(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        _chatClient.VerifyNoOtherCalls();

        Assert.NotNull(actualCompletion);
        Assert.NotEmpty(actualCompletion);
        Assert.Equal(expectedCompletion, actualCompletion);
    }

    [Fact]
    public async Task GetCompletionWithMultiMessageResponseReturnsJoinedString()
    {
        var expectedImageBytes = new Faker().Random.Bytes(100);
        const string expectedImageMime = "image/png";
        var expectedSettings = FakeChatClientSettings.Build();
        IEnumerable<string> expectedCompletions = [new Faker().Lorem.Sentences(), new Faker().Lorem.Sentences()];

        _chatClient.Setup(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new ChatResponse([.. expectedCompletions.Select(c => new ChatMessage(ChatRole.Assistant, c))]));

        var actualCompletion = await Sut.GetCompletion(expectedImageBytes, expectedImageMime, TestContext.Current.CancellationToken);

        _chatClient.Verify(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        _chatClient.VerifyNoOtherCalls();

        Assert.NotNull(actualCompletion);
        Assert.NotEmpty(actualCompletion);
        Assert.Equal(string.Join('\n', expectedCompletions), actualCompletion);
    }
}
