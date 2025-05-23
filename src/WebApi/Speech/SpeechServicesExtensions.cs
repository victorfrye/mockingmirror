using Microsoft.CognitiveServices.Speech;

namespace VictorFrye.MockingMirror.WebApi.Speech;

/// <summary>
/// Provides extension methods for registering speech services in the services collection provided by the <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class SpeechServicesExtensions
{
    /// <summary>
    /// Adds speech services in the <see cref="IServiceCollection"/> provided by the <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to read config from and add services to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> that can be used to register additional services.</returns>
    public static IHostApplicationBuilder AddSpeechServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddOptions<SpeechClientSettings>()
                        .Bind(builder.Configuration.GetSection(SpeechClientSettings.ConfigurationSectionName))
                        .ValidateDataAnnotations()
                        .ValidateOnStart();

        builder.Services.AddScoped<ISpeechService, SpeechService>();

        return builder;
    }
}
