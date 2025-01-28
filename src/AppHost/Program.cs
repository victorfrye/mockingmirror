var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.WebApi>("WebApi")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("WebClient", "../WebClient")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(env: "VITE_PORT")
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();