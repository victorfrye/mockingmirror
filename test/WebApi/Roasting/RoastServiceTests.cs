using VictorFrye.MockingMirror.WebApi.ChatCompletion;
using VictorFrye.MockingMirror.WebApi.Roasting;
using VictorFrye.MockingMirror.WebApi.Speech;

namespace VictorFrye.MockingMirror.WebApi.Tests.Roasting;

public class RoastServiceTests
{
    private readonly Mock<IChatService> _chatServiceMock = new();
    private readonly Mock<ISpeechService> _speechServiceMock = new();

    private RoastService Sut => new(_chatServiceMock.Object);

    [Fact]
    public async Task AddRoastWithoutSpeechReturnsTextOnlyResult()
    {
        var expectedRoast = FakeRoast.Build(includeSpeech: false);
        Assert.False(expectedRoast.IncludeSpeech);

        _chatServiceMock.Setup(x => x.GetCompletion(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Faker().Lorem.Sentences(3));

        var actualRoast = await Sut.AddRoast(expectedRoast, TestContext.Current.CancellationToken);

        _chatServiceMock.Verify(x => x.GetCompletion(expectedRoast.ImageBytes, expectedRoast.ImageMime, It.IsAny<CancellationToken>()), Times.Once());
        _chatServiceMock.VerifyNoOtherCalls();

        _speechServiceMock.Verify(x => x.GetSpeech(It.IsAny<string>()), Times.Never());
        _speechServiceMock.VerifyNoOtherCalls();

        Assert.NotNull(actualRoast);
        Assert.Equal(expectedRoast.Id, actualRoast.Id);
        Assert.Equal(expectedRoast.ImageBytes, actualRoast.ImageBytes);
        Assert.Equal(expectedRoast.ImageMime, actualRoast.ImageMime);
        Assert.NotNull(actualRoast.CompletionText);
        Assert.NotEmpty(actualRoast.CompletionText);
        Assert.NotNull(actualRoast.SpeechBytes);
        Assert.Empty(actualRoast.SpeechBytes);
    }

    [Fact]
    public async Task AddRoastWithSpeechIncludedReturnsResultWithAudio()
    {
        var expectedRoast = FakeRoast.Build(includeSpeech: true);
        Assert.True(expectedRoast.IncludeSpeech);

        _chatServiceMock.Setup(x => x.GetCompletion(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Faker().Lorem.Sentences(3));
        _speechServiceMock.Setup(x => x.GetSpeech(It.IsAny<string>()))
                          .ReturnsAsync(new Faker().Random.Bytes(100));

        var actualRoast = await Sut.AddRoast(expectedRoast, TestContext.Current.CancellationToken);

        _chatServiceMock.Verify(x => x.GetCompletion(expectedRoast.ImageBytes, expectedRoast.ImageMime, It.IsAny<CancellationToken>()), Times.Once());
        _chatServiceMock.VerifyNoOtherCalls();

        _speechServiceMock.Verify(x => x.GetSpeech(It.IsAny<string>()), Times.Once());
        _speechServiceMock.VerifyNoOtherCalls();

        Assert.NotNull(actualRoast);
        Assert.Equal(expectedRoast.Id, actualRoast.Id);
        Assert.Equal(expectedRoast.ImageBytes, actualRoast.ImageBytes);
        Assert.Equal(expectedRoast.ImageMime, actualRoast.ImageMime);
        Assert.NotNull(actualRoast.CompletionText);
        Assert.NotEmpty(actualRoast.CompletionText);
        Assert.NotNull(actualRoast.SpeechBytes);
        Assert.NotEmpty(actualRoast.SpeechBytes);
    }
}
