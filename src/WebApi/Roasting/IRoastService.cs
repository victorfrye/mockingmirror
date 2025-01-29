namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal interface IRoastService
{
    public Task<RoastResponse> AddRoast(RoastRequest request);
}
