namespace VictorFrye.MockingMirror.WebApi.Roasting;

internal static class RoastEndpoints
{
    internal static void MapRoastingEndpoints(this WebApplication app)
    {
        app.MapPost("/roasts", PostRoast);
    }

    internal static async Task<IResult> PostRoast(IRoastService service, RoastRequest request, CancellationToken cancellationToken)
    {
        var response = await service.AddRoast(request, cancellationToken);
        return Results.Created($"/roasts/{response.Id}", response);
    }
}
