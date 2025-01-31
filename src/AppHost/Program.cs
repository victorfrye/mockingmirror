var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.WebApi>("api")
    .WithExternalHttpEndpoints();

builder.AddNpmApp("client", "../WebClient", "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("VITE_API_BASEURL", api.GetEndpoint("https"))
    .WithHttpEndpoint(port: 5173, env: "VITE_PORT")
    .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
