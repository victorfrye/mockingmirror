using Azure;
using Azure.AI.OpenAI;

using Microsoft.Extensions.AI;

namespace VictorFrye.MockingMirror.WebApi.Chat;

public class ChatClientFactory : IChatClientFactory
{
    public IChatClient Create(ChatClientSettings settings, ChatClientKind kind) => kind switch
    {
        ChatClientKind.OpenAI => new AzureOpenAIClient(new Uri(settings.Endpoint), new AzureKeyCredential(settings.ApiKey)).GetChatClient(settings.DeploymentName).AsIChatClient(),
        _ => throw new NotSupportedException($"Chat client kind '{kind}' is not supported."),
    };
}
