var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.MuseumSystem_Api>("museum-api");

builder.Build().Run();