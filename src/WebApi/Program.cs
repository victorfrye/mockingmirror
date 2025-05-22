using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.OpenApi.Models;

using VictorFrye.MockingMirror.Extensions.ServiceDefaults;
using VictorFrye.MockingMirror.WebApi.ChatCompletion;
using VictorFrye.MockingMirror.WebApi.Roasting;
using VictorFrye.MockingMirror.WebApi.Speech;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(static options =>
    options.AddDefaultPolicy(static policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()));

builder.AddServiceDefaults();
builder.AddChatClient("llm");

var config = builder.Configuration;

// builder.Services.AddOptions<ChatClientSettings>()
//                 .Bind(config.GetSection(ChatClientSettings.ConfigurationSectionName))
//                 .ValidateDataAnnotations()
//                 .ValidateOnStart();

// builder.Services.AddOptions<SpeechClientSettings>()
//                 .Bind(config.GetSection(SpeechClientSettings.ConfigurationSectionName))
//                 .ValidateDataAnnotations()
//                 .ValidateOnStart();

builder.Services.AddScoped<IChatService, ChatService>()
                .AddScoped<ISpeechService, SpeechService>()
                .AddScoped<IRoastService, RoastService>();

builder.Services.AddControllers()
    .AddJsonOptions(static options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
        options.JsonSerializerOptions.AllowTrailingCommas = true;
    });

builder.Services.AddOpenApi(static options =>
{
    options.AddDocumentTransformer(static (document, _, _) =>
    {
        document.Info = new()
        {
            Title = "Mocking Mirror API",
            Version = "v1",
            Description = "Web API for roasting people with Azure AI services."
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

app.MapRoastEndpoints();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

await app.RunAsync();
