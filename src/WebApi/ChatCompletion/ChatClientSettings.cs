using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.ChatCompletion;

/// <summary>
/// The settings relevant to accessing the chat client.
/// </summary>
public sealed class ChatClientSettings
{
    internal const string ConfigurationSectionName = nameof(ChatClientSettings);

    /// <summary>
    /// Gets or sets a <see cref="Uri"/> referencing the endpoint of the chat client.
    /// For Azure OpenAI, this might look like "https://{{account_name}}.openai.azure.com/".
    /// </summary>
    [Url]
    public Uri? Endpoint { get; set; }

    /// <summary>
    /// Gets or sets the key to use to authenticate to the chat client endpoint.
    /// </summary>
    public string? ApiKey { get; set; }

    /// <summary>
    /// Gets or sets the name of the deployed model to use for the chat client.
    /// </summary>
    public string? DeploymentName { get; set; }
}
