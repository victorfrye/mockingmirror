using VictorFrye.MockingMirror.WebApi.ChatCompletion;
using VictorFrye.MockingMirror.WebApi.Roasting;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Tests.Roasting;

public class RoastServiceTests
{
    private readonly Mock<IChatService> _chatServiceMock = new();
    private readonly Mock<ISpeechService> _speechServiceMock = new();

    private RoastService Sut => new(_chatServiceMock.Object, _speechServiceMock.Object);

    [Fact]
    public async Task AddRoastWithoutSpeechReturnsTextOnlyResult()
    {
        var expectedRequest = FakeRoast.BuildRequest(includeSpeech: false);
        Assert.False(expectedRequest.IncludeSpeech);

        var expectedCompletion = new Faker().Lorem.Sentences(3);

        _chatServiceMock.Setup(x => x.GetCompletion(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedCompletion);

        var actualRoast = await Sut.AddRoast(expectedRequest, TestContext.Current.CancellationToken);

        _chatServiceMock.Verify(x => x.GetCompletion(expectedRequest.ImageBytes, expectedRequest.ImageMime, It.IsAny<CancellationToken>()), Times.Once());
        _chatServiceMock.VerifyNoOtherCalls();

        _speechServiceMock.Verify(x => x.GetSpeech(It.IsAny<string>()), Times.Never());
        _speechServiceMock.VerifyNoOtherCalls();

        Assert.NotNull(actualRoast);
        Assert.Equal(expectedCompletion, actualRoast.CompletionText);
        Assert.Null(actualRoast.SpeechBytes);
    }

    [Fact]
    public async Task AddRoastWithSpeechIncludedReturnsResultWithAudio()
    {
        var expectedRoast = FakeRoast.BuildRequest(includeSpeech: true);
        Assert.True(expectedRoast.IncludeSpeech);

        var expectedCompletion = new Faker().Lorem.Sentences(3);
        var expectedSpeech = new Faker().Random.Bytes(100);

        _chatServiceMock.Setup(x => x.GetCompletion(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(expectedCompletion);
        _speechServiceMock.Setup(x => x.GetSpeech(It.IsAny<string>()))
                          .ReturnsAsync(expectedSpeech);

        var actualRoast = await Sut.AddRoast(expectedRoast, TestContext.Current.CancellationToken);

        _chatServiceMock.Verify(x => x.GetCompletion(expectedRoast.ImageBytes, expectedRoast.ImageMime, It.IsAny<CancellationToken>()), Times.Once());
        _chatServiceMock.VerifyNoOtherCalls();

        _speechServiceMock.Verify(x => x.GetSpeech(It.IsAny<string>()), Times.Once());
        _speechServiceMock.VerifyNoOtherCalls();

        Assert.NotNull(actualRoast);
        Assert.Equal(expectedCompletion, actualRoast.CompletionText);
        Assert.Equal(expectedSpeech, actualRoast.SpeechBytes);
    }
}
