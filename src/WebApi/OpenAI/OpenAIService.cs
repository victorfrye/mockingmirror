using Azure;
using Azure.AI.OpenAI;

using Microsoft.Extensions.Options;

using OpenAI.Chat;

namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal class OpenAIService(IOptions<OpenAIServiceOptions> options) : IOpenAIService
{
    private readonly OpenAIServiceOptions _options = options.Value;

    private AzureOpenAIClient Client => new(_options.Endpoint, new AzureKeyCredential(_options.ApiKey));

    private ChatClient ChatClient => Client.GetChatClient(_options.DeploymentName);

    public async Task<string> GetCompletion(string prompt, string imageBytes, string imageMime = "image/png")
    {
        var imageUri = new Uri($"data:{imageMime};base64,{imageBytes}");

        List<ChatMessage> messages = [
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(prompt),
                ChatMessageContentPart.CreateImagePart(imageUri, imageMime))
            ];

        ChatCompletion completion = await ChatClient.CompleteChatAsync(messages);

        return completion.Content[0].Text;
    }
}