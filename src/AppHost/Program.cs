var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.WebApi>("webapi");

builder.Build().Run();
