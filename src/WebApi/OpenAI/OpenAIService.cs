using Azure;
using Azure.AI.OpenAI;

using Microsoft.Extensions.Options;

using OpenAI.Chat;

namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal class OpenAIService(IOptions<OpenAIServiceOptions> options) : IOpenAIService
{
    private readonly OpenAIServiceOptions _options = options.Value;

    private AzureOpenAIClient Client => new(new Uri(_options.Endpoint), new AzureKeyCredential(_options.ApiKey));

    private ChatClient ChatClient => Client.GetChatClient(_options.DeploymentName);

    private const string Prompt =
        """
        You are a sentient mirror that interacts with users who stand in front of you.

        The user will provide you with a picture of themselves and ask you to mock them
        in a friendly but humorous manner.

        It is very important that you do not mention people or faces being mysterious,
        blurred, classified, hidden, or pixelated.

        For example you should not say "The person's face is blurred out" or
        "The person's face is hidden" or refer to "incognito mode" or mention
        "invisible faces" or "cloak of invisibility".

        Keep your response to two or three sentences.

        Some example responses include:
          1. Like a steamroller flattened a cat that had already been left out on the roof too long.
          2. Well, with that white hat you really should be wearing something a bit more summery, don't you think?
          3. Those dark eyes are so noticeable your mom will be asking if you're tired.
          4. The 1980s called. They want their hair and shoulder pads back.

        Respond humorously to the picture of the user.

        Here is the picture they provided:
        """;

    private readonly ChatCompletionOptions ChatOptions = new()
    {
        Temperature = 1.2f,
        MaxOutputTokenCount = 500,
    };

    public async Task<string> GetCompletion(
        byte[] imageBytes,
        string imageMime,
        CancellationToken cancellationToken = default)
    {
        var imageData = BinaryData.FromBytes(imageBytes);

        IEnumerable<ChatMessage> messages = [
            new UserChatMessage(
                ChatMessageContentPart.CreateTextPart(Prompt),
                ChatMessageContentPart.CreateImagePart(imageData, imageMime))
            ];

        ChatCompletion completion = await ChatClient.CompleteChatAsync(messages, ChatOptions, cancellationToken);

        return completion.Content[0].Text;
    }
}
