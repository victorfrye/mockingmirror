namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal static class RoastEndpoints
{
    internal static void MapRoastingEndpoints(this WebApplication app)
    {
        app.MapPostRoastEndpoint();
    }

    private static void MapPostRoastEndpoint(this WebApplication app)
    {
        app.MapPost("/roasts", async (IRoastService service, RoastRequest request) =>
        {
            var response = await service.AddRoast(request);
            return Results.Created($"/roasts/{response.Id}", response);
        });
    }
}
