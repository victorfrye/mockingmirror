using Aspire.Azure.AI.OpenAI;

using Azure.AI.OpenAI;
using Azure.Core.Extensions;

using CommunityToolkit.Aspire.OllamaSharp;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using VictorFrye.MockingMirror.WebApi.ChatCompletion;

namespace VictorFrye.MockingMirror.WebApi.Tests.ChatCompletion;

public class ChatClientBuilderExtensionsTests
{
    private Mock<IHostApplicationBuilder> _builderMock = new();
    private Mock<IConfigurationManager> _configMock = new();
    private Mock<IConfigurationSection> _sectionMock = new();

    [Theory]
    [InlineData(ChatProvider.Ollama)]
    [InlineData(ChatProvider.OpenAI)]
    public void CreateReturnsExpectedClientType(ChatProvider provider)
    {
        var expectedConnectionName = new Faker().Random.AlphaNumeric(10);
        var expectedSettings = FakeChatClientSettings.Build();

        _builderMock.SetupGet(x => x.Configuration).Returns(_configMock.Object);
        _configMock.Setup(x => x.GetSection("ConnectionStrings")[It.IsAny<string>()]).Returns($"Provider={provider};");

        var client = _builderMock.Object.AddChatClient(expectedConnectionName);

        switch (provider)
        {
            case ChatProvider.Ollama:
                _builderMock.Verify(x => x.AddOllamaApiClient(expectedConnectionName, It.IsAny<Action<OllamaSharpSettings>>()), Times.Once);
                break;
            case ChatProvider.OpenAI:
                _builderMock.Verify(x => x.AddAzureOpenAIClient(expectedConnectionName, It.IsAny<Action<AzureOpenAISettings>>(), It.IsAny<Action<IAzureClientBuilder<AzureOpenAIClient, AzureOpenAIClientOptions>>>()), Times.Once);
                break;
            default:
                throw new NotSupportedException($"Unsupported provider: {provider}");
        }
    }


}
