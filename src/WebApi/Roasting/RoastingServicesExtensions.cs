namespace VictorFrye.MockingMirror.WebApi.Roasting;

/// <summary>
/// Provides extension methods fo registering roasting services in the services collection provided by the <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class RoastingServicesExtensions
{
    /// <summary>
    /// Adds roasting services in the <see cref="IServiceCollection"/> provided by the <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to read config from and add services to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> that can be used to register additional services.</returns>
    public static IHostApplicationBuilder AddRoastingServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRoastService, RoastService>();

        return builder;
    }
}
