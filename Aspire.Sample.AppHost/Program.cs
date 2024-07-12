var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var postgresDb = builder.AddPostgres("postgres")
    // Set the name of the default database to auto-create on container startup.
    .WithEnvironment("POSTGRES_DB", "forecasts")
    // Add the default database to the application model so that it can be referenced by other resources.
    .AddDatabase("forecasts");

var apiService = builder.AddProject<Projects.Aspire_Sample_ApiService>("apiservice")
    .WithReference(postgresDb);

builder.AddProject<Projects.Aspire_Sample_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WithReference(apiService)
    .WithReference(postgresDb);

var app = builder.Build();

//var cstr = await postgresDb.Resource.Parent.GetConnectionStringAsync();

//Console.WriteLine("Connection string: {0}", cstr);

app.Run();
