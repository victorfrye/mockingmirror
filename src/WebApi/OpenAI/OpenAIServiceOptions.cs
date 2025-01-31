using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.OpenAI;

internal class OpenAIServiceOptions
{
    public const string ConfigurationSectionName = nameof(OpenAIServiceOptions);

    [Required]
    [Url]
    public required string Endpoint { get; set; }

    [Required]
    public required string ApiKey { get; set; }

    [Required]
    public required string DeploymentName { get; set; }
}
