namespace VictorFrye.MockingMirror.WebApi.Roasting;

/// <summary>
/// Provides extension methods for adding <see cref="RouteEndpoint"/>s related to roasting in the <see cref="WebApplication"/>.
/// </summary>
public static class RoastEndpoints
{
    internal const string BasePath = "/roasts";

    /// <summary>
    /// Register endpoints onto the current application for generating roasts using AI.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to register the endpoints on.</param>
    public static void MapRoastEndpoints(this WebApplication app)
    {
        app.MapPost(BasePath, PostRoast);
    }

    /// <summary>
    /// The delegate for the POST endpoint to create a new roast.
    /// </summary>
    /// <param name="service">The <see cref="IRoastService"/> to orchestrate and control flow for roasting.</param>
    /// <param name="request">The <see cref="RoastRequest"/> containing the image data to roast.</param>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to monitor for cancellation requests. The default is <see cref="CancellationToken.None"/>.
    public static async Task<IResult> PostRoast(IRoastService service, RoastRequest request, CancellationToken cancellationToken)
    {
        var response = await service.AddRoast(request, cancellationToken);
        return Results.Created($"{BasePath}/{response.Id}", response);
    }
}
