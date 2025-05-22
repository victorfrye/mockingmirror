using Microsoft.Extensions.AI;

namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

public class ChatService(IChatClient client) : IChatService
{
    private const string SystemPrompt =
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
        """;

    private const string UserPrompt = "Here is the picture to roast me:";

    private readonly ChatOptions ChatOptions = new()
    {
        Temperature = 1.2f,
        MaxOutputTokens = 500,
    };

    public async Task<string> GetCompletion(
        byte[] imageBytes,
        string imageMime,
        CancellationToken cancellationToken = default)
    {
        var imageData = BinaryData.FromBytes(imageBytes);

        IEnumerable<ChatMessage> messages = [
            new ChatMessage(
                ChatRole.System, SystemPrompt),
            new ChatMessage(
                ChatRole.User,
                [
                    new TextContent(UserPrompt),
                    new DataContent(imageData, imageMime),
                ]
            )
        ];

        ChatResponse response = await client.GetResponseAsync(messages, ChatOptions, cancellationToken);

        return string.Join('\n', response.Messages.Select(m => m.Text));
    }
}
