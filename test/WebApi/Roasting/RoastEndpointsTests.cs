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
        var expectedRequest = FakeRoast.BuildRequest();
        var expectedResponse = FakeRoast.BuildResponse();

        _serviceMock.Setup(x => x.AddRoast(It.IsAny<RoastRequest>(), It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

        var actual = await RoastEndpoints.PostRoast(_serviceMock.Object, expectedRequest, TestContext.Current.CancellationToken);

        _serviceMock.Verify(x => x.AddRoast(expectedRequest, It.IsAny<CancellationToken>()), Times.Once);
        _serviceMock.VerifyNoOtherCalls();

        Assert.NotNull(actual);
        var result = Assert.IsType<Created<RoastResponse>>(actual);
        Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        Assert.Equal($"{RoastEndpoints.BasePath}/{expectedResponse.Id}", result.Location);
        Assert.NotNull(result.Value);
        var actualRoast = Assert.IsType<RoastResponse>(result.Value);
        Assert.Equal(expectedResponse.Id, actualRoast.Id);
    }
}
