using Aspire.Sample.Data;
using Aspire.Sample.MigrationService;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

//builder.Services.AddHostedService<ApiDbInitializer>();

builder.AddServiceDefaults();

//builder.Services.AddOpenTelemetry()
//    .WithTracing(tracing => tracing.AddSource(ApiDbInitializer.ActivitySourceName));

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

var app = builder.Build();

app.Run();