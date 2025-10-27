var builder = DistributedApplication.CreateBuilder(args);

builder.AddDockerComposeEnvironment("docker");
// builder.AddAzureContainerAppEnvironment("aca");

var oaiName = builder.AddParameter("OpenAIName");
var oaiResourceGroup = builder.AddParameter("OpenAIResourceGroup");
var oaiModel = builder.AddParameter("OpenAIModel");
var speechKey = builder.AddParameter("SpeechKey", secret: true);
var speechRegion = builder.AddParameter("SpeechRegion");

var openai = builder.AddAzureOpenAI("openai")
                    .AsExisting(oaiName, oaiResourceGroup);

var api = builder.AddProject<Projects.WebApi>("api")
                 .WithReference(openai)
                 .WaitFor(openai)
                 .WithEnvironment("ChatClientSettings__DeploymentName", oaiModel)
                 .WithEnvironment("SpeechClientSettings__ApiKey", speechKey)
                 .WithEnvironment("SpeechClientSettings__Region", speechRegion)
                 .WithHttpHealthCheck("/alive")
                 .WithExternalHttpEndpoints()
                 .PublishAsDockerFile(b => b.WithDockerfile("../..", "./src/WebApi/Dockerfile"));

builder.AddNpmApp("client", "../WebClient", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("NEXT_PUBLIC_API_BASEURL", api.GetEndpoint("https"))
       .WithHttpEndpoint(env: "PORT")
       .WithHttpHealthCheck("/")
       .WithExternalHttpEndpoints()
       .PublishAsDockerFile(b => b.WithDockerfile("../..", "./src/WebClient/Dockerfile"));

await builder.Build().RunAsync();
