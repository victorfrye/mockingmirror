using System.Text.Json;

using Microsoft.OpenApi.Models;

using VictorFrye.MockingMirror.Extensions.ServiceDefaults;
using VictorFrye.MockingMirror.WebApi.OpenAI;
using VictorFrye.MockingMirror.WebApi.Roasting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var config = builder.Configuration;

builder.Services.AddOptions<OpenAIServiceOptions>()
                .Bind(builder.Configuration.GetSection(OpenAIServiceOptions.ConfigurationSectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

builder.Services.AddScoped<IOpenAIService, OpenAIService>()
                .AddScoped<IRoastService, RoastService>();

builder.Services.AddControllers()
    .AddJsonOptions(static options =>
    {
        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
    });

builder.Services.AddOpenApi(static options =>
{
    options.AddDocumentTransformer(static (document, _, _) =>
    {
        document.Info = new()
        {
            Title = "Victor Frye AIaaS Demo API",
            Version = "v1",
            Description = "Web API for roasting people with Azure AIaaS."
        };
        return Task.CompletedTask;
    });

    options.AddOperationTransformer(static (operation, _, _) =>
    {
        operation.Responses.Add("400", new OpenApiResponse { Description = "Bad request" });
        operation.Responses.Add("500", new OpenApiResponse { Description = "Internal server error" });
        return Task.CompletedTask;
    });
});



var app = builder.Build();

app.MapDefaultEndpoints();
app.MapOpenApi()
   .CacheOutput();

app.MapRoastingEndpoints();

app.UseHttpsRedirection();
app.UseAuthorization();

await app.RunAsync();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}