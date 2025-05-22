var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddAzureOpenAI("openai")
                    .AddDeployment("gpt-4o", "gpt-4o", "2024-11-20");

// var llm = builder.AddLlm("llm")
//               //    .RunAsOllama("phi4-mini", static c => c.WithLifetime(ContainerLifetime.Persistent))
//                  .RunAsOpenAI("gpt-4o", "2024-11-20")
//                  .PublishAsOpenAI("gpt-4o", "2024-11-20");

var api = builder.AddProject<Projects.WebApi>("api")
                 .WithReference(openai)
                 .WaitFor(openai)
                 .WithHttpHealthCheck("/alive")
                 .WithExternalHttpEndpoints();

builder.AddNpmApp("client", "../WebClient", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("NEXT_PUBLIC_API_BASEURL", api.GetEndpoint("https"))
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
