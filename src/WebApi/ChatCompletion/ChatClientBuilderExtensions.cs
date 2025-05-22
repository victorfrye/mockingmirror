namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

public static class ChatClientBuilderExtensions
{
    public static IHostApplicationBuilder AddChatClient(this IHostApplicationBuilder builder, string connectionName)
    {
        var connectionString = builder.Configuration.GetConnectionString(connectionName);

        if (!ChatClientSettings.TryParse(connectionString, out var settings))
        {
            throw new InvalidOperationException($"Invalid connection string: {connectionString} Expected format: Endpoint=<endpoint>;ApiKey=<api-key>;Model=<model>;Provider=<ollama/openai>;");
        }

        var _ = settings.Provider switch
        {
            ChatProvider.Ollama => builder.AddOllamaApiClient(connectionName).AddChatClient(),
            ChatProvider.OpenAI => builder.AddAzureOpenAIClient(connectionName).AddChatClient(settings.Model),
            _ => throw new NotSupportedException($"Unsupported provider: {settings.Provider}. Expected: {nameof(ChatProvider.Ollama)} or {nameof(ChatProvider.OpenAI)}"),
        };

        return builder;
    }
}
