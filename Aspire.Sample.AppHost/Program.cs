var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgresDb = builder.AddPostgres("postgres")
    // Set the name of the default database to auto-create on container startup.
    .WithEnvironment("POSTGRES_DB", "forecasts")
    // Add the default database to the application model so that it can be referenced by other resources.
    .AddDatabase("forecasts");

var apiService = builder.AddProject<Projects.Aspire_Sample_ApiService>("api")
    .WithReference(postgresDb);

builder.AddProject<Projects.Aspire_Sample_MigrationService>("migrator")
       .WithReference(postgresDb);

builder.AddProject<Projects.Aspire_Sample_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(postgresDb);

builder.AddNpmApp("vue", "../Aspire.Sample.Vue")
    .WithReference(apiService)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

var app = builder.Build();

app.Run();
