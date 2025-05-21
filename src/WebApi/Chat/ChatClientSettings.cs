using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.Chat;

public class ChatClientSettings
{
    public const string ConfigurationSectionName = nameof(ChatClientSettings);

    [Required]
    [Url]
    public required string Endpoint { get; set; }

    [Required]
    public required string ApiKey { get; set; }

    [Required]
    public required string DeploymentName { get; set; }
}
