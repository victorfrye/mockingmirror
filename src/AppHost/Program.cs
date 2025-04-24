var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.WebApi>("api")
                 .WithHttpsHealthCheck("/alive")
                 .WithExternalHttpEndpoints();

builder.AddNpmApp("client", "../WebClient", "dev")
       .WithReference(api)
       .WaitFor(api)
       .WithEnvironment("MOCKINGMIRROR_API_BASEURL", api.GetEndpoint("https"))
       .WithHttpEndpoint(env: "PORT")
       .WithExternalHttpEndpoints();

await builder.Build().RunAsync();
