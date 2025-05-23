namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

/// <summary>
/// Provides extension methods for registering chat completion services in the services collection provided by the <see cref="IHostApplicationBuilder"/>.
/// </summary>
public static class ChatCompletionServicesExtensions
{
    /// <summary>
    /// Adds chat completion services in the <see cref="IServiceCollection"/> provided by the <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder">The <see cref="IHostApplicationBuilder"/> to read config from and add services to.</param>
    /// <returns>The <see cref="IHostApplicationBuilder"/> that can be used to register additional services.</returns>
    public static IHostApplicationBuilder AddChatCompletionServices(this IHostApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSection(ChatClientSettings.ConfigurationSectionName)
                                            .Get<ChatClientSettings>();

        builder.AddAzureOpenAIClient("openai")
               .AddChatClient(settings?.DeploymentName);

        builder.Services.AddScoped<IChatService, ChatService>();

        return builder;
    }
}
