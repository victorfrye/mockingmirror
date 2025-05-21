using Microsoft.Extensions.AI;

namespace VictorFrye.MockingMirror.WebApi.Chat;

public interface IChatClientFactory
{
    IChatClient Create(ChatClientSettings settings, ChatClientKind kind);
}
