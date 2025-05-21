using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

using VictorFrye.MockingMirror.WebApi.Roasting;

namespace VictorFrye.MockingMirror.WebApi.Tests.Roasting;

public class RoastEndpointsTests
{
    private readonly Mock<IRoastService> _serviceMock = new();

    [Fact]
    public async Task PostRoastReturnsCreated()
    {
        var expectedRoast = FakeRoast.Build();

        _serviceMock.Setup(x => x.AddRoast(It.IsAny<Roast>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedRoast);

        var actual = await RoastEndpoints.PostRoast(_serviceMock.Object, expectedRoast, TestContext.Current.CancellationToken);

        _serviceMock.Verify(x => x.AddRoast(expectedRoast, It.IsAny<CancellationToken>()), Times.Once);
        _serviceMock.VerifyNoOtherCalls();

        Assert.NotNull(actual);
        var result = Assert.IsType<Created<Roast>>(actual);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal($"{RoastEndpoints.BasePath}/{expectedRoast.Id}", result.Location);
        Assert.NotNull(result.Value);
        var actualRoast = Assert.IsType<Roast>(result.Value);
        Assert.Equal(expectedRoast.Id, actualRoast.Id);
    }
}
