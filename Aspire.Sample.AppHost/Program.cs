var builder = DistributedApplication.CreateBuilder(args);

// Maintain a consistent password across launches, so can keep data consitent across launches
var postgresPassword = builder.AddParameter("postgres-password", secret: true);

var postgresDb = builder.AddPostgres("postgres", password:postgresPassword)
    // Set the name of the default database to auto-create on container startup.
    .WithEnvironment("POSTGRES_DB", "forecasts")
    // Persist the data across launches
    .WithDataVolume()
    // Add the default database to the application model so that it can be referenced by other resources.
    .AddDatabase("forecasts");

var apiService = builder.AddProject<Projects.Aspire_Sample_BackEnd>("backend")
    .WithReference(postgresDb);

builder.AddProject<Projects.Aspire_Sample_Worker>("worker")
       .WithReference(postgresDb);

builder.AddProject<Projects.Aspire_Sample_MigrationService>("migrator")
       .WithReference(postgresDb);

// Blazor front-end
#if false
var cache = builder.AddRedis("cache");
builder.AddProject<Projects.Aspire_Sample_Web>("web")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(postgresDb);
#endif

builder.AddNpmApp("frontend", "../Aspire.Sample.FrontEnd")
    .WithReference(apiService)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();

var app = builder.Build();

await app.RunAsync();
