using VictorFrye.MockingMirror.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var llm = builder.AddLlm("llm")
                 .RunAsOllama("phi4", static c => c.WithLifetime(ContainerLifetime.Persistent))
                 .PublishAsOpenAI("gpt-4o", "2024-10-01");

var api = builder.AddProject<Projects.WebApi>("api")
                 .WithReference(llm)
                 .WaitFor(llm)
                 .WithHttpHealthCheck("/alive")
                 .WithExternalHttpEndpoints();

builder.AddNpmApp("client", "../WebClient", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("NEXT_PUBLIC_API_BASEURL", api.GetEndpoint("https"))
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
