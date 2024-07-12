using Aspire.Sample.Data;
using Aspire.Sample.MigrationService;

// Inpsired by: https://github.com/dotnet/aspire-samples/tree/main/samples/DatabaseMigrations/DatabaseMigrations.MigrationService

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.AddServiceDefaults();

//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracing => tracing.AddSource(ApiDbInitializer.ActivitySourceName));

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

var app = builder.Build();

app.Run();
