using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;

using VictorFrye.MockingMirror.WebApi.Chat;

namespace VictorFrye.MockingMirror.WebApi.Tests.Chat;

public class OpenAIChatServiceTests
{
    private readonly Mock<IChatClientFactory> _factoryMock = new();
    private readonly Mock<IChatClient> _chatClient = new();
    private readonly Mock<IOptionsSnapshot<ChatClientSettings>> _optionsMock = new();

    private OpenAIChatService Sut => new(_factoryMock.Object, _optionsMock.Object);

    [Fact]
    public async Task GetCompletionWithSingleMessageResponseReturnsString()
    {
        var expectedImageBytes = new Faker().Random.Bytes(100);
        const string expectedImageMime = "image/png";
        var expectedSettings = FakeChatClientSettings.Build();
        var expectedCompletion = new Faker().Lorem.Sentences();

        _optionsMock.SetupGet(x => x.Value).Returns(expectedSettings);
        _factoryMock.Setup(x => x.Create(It.IsAny<ChatClientSettings>(), ChatClientKind.OpenAI))
                    .Returns(_chatClient.Object);
        _chatClient.Setup(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new ChatResponse(
                   [
                       new ChatMessage(ChatRole.Assistant, expectedCompletion)
                   ]));

        var actualCompletion = await Sut.GetCompletion(expectedImageBytes, expectedImageMime, TestContext.Current.CancellationToken);

        _optionsMock.Verify(x => x.Value, Times.Once());
        _optionsMock.VerifyNoOtherCalls();

        _factoryMock.Verify(x => x.Create(expectedSettings, ChatClientKind.OpenAI), Times.Once());
        _factoryMock.VerifyNoOtherCalls();

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

        _optionsMock.SetupGet(x => x.Value).Returns(expectedSettings);
        _factoryMock.Setup(x => x.Create(It.IsAny<ChatClientSettings>(), ChatClientKind.OpenAI))
                    .Returns(_chatClient.Object);
        _chatClient.Setup(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()))
                   .ReturnsAsync(new ChatResponse([.. expectedCompletions.Select(c => new ChatMessage(ChatRole.Assistant, c))]));

        var actualCompletion = await Sut.GetCompletion(expectedImageBytes, expectedImageMime, TestContext.Current.CancellationToken);

        _optionsMock.Verify(x => x.Value, Times.Once());
        _optionsMock.VerifyNoOtherCalls();

        _factoryMock.Verify(x => x.Create(expectedSettings, ChatClientKind.OpenAI), Times.Once());
        _factoryMock.VerifyNoOtherCalls();

        _chatClient.Verify(x => x.GetResponseAsync(It.IsAny<IEnumerable<ChatMessage>>(), It.IsAny<ChatOptions>(), It.IsAny<CancellationToken>()), Times.Once());
        _chatClient.VerifyNoOtherCalls();

        Assert.NotNull(actualCompletion);
        Assert.NotEmpty(actualCompletion);
        Assert.Equal(string.Join('\n', expectedCompletions), actualCompletion);
    }
}
