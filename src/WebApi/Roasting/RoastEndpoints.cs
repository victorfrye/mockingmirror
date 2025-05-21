namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal static class RoastEndpoints
{
    internal const string BasePath = "/roasts";

    internal static void MapRoastingEndpoints(this WebApplication app)
    {
        app.MapPost(BasePath, PostRoast);
    }

    internal static async Task<IResult> PostRoast(IRoastService service, Roast roast, CancellationToken cancellationToken)
    {
        var response = await service.AddRoast(roast, cancellationToken);
        return Results.Created($"{BasePath}/{response.Id}", response);
    }
}
