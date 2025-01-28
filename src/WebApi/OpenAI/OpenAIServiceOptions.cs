using System.ComponentModel.DataAnnotations;

namespace VictorFrye.MockingMirror.WebApi.OpenAI;

public class OpenAIServiceOptions
{
    public const string ConfigurationSectionName = nameof(OpenAIServiceOptions);

    [Required]
    [Url]
    public required Uri Endpoint { get; set; }

    [Required]
    public required string ApiKey { get; set; }

    public string DeploymentName { get; set; } = "gpt-4o mini";
}