using Azure;
using Azure.AI.OpenAI;

using Microsoft.Extensions.Options;

using OpenAI.Chat;

namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal class OpenAIService(IOptions<OpenAIServiceOptions> options) : IOpenAIService
{
    private readonly OpenAIServiceOptions _options = options.Value;

    private AzureOpenAIClient Client => new(new(_options.Endpoint), new AzureKeyCredential(_options.ApiKey));

    private ChatClient ChatClient => Client.GetChatClient(_options.DeploymentName);

    private readonly ChatCompletionOptions ChatOptions = new()
    {
        Temperature = (float)0.2,
        MaxOutputTokenCount = 500,
    };

    public async Task<string> GetCompletion(string prompt, byte[] imageBytes, string imageMime, CancellationToken cancellationToken = default)
    {
        var imageData = BinaryData.FromBytes(imageBytes);

        List<ChatMessage> messages = [
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(prompt),
                ChatMessageContentPart.CreateImagePart(imageData, imageMime))
            ];

        ChatCompletion completion = await ChatClient.CompleteChatAsync(messages, ChatOptions, cancellationToken);

        return completion.Content[0].Text;
    }
}
