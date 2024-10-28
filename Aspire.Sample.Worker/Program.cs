using Aspire.Sample.Data;
using Aspire.Sample.Worker;
using Aspire.Sample.Worker.Api;
using Aspire.Sample.Worker.Options;
using System.Reflection;

var builder = Host.CreateApplicationBuilder(args);

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

builder.Services.Configure<WeatherOptions>(
    builder.Configuration.GetSection(WeatherOptions.Section)
);

builder.Services.AddDataProvider();
builder.Services.AddApplicationFeatures();

builder.Services.AddHttpClient<WeatherClient>();
builder.Services.AddHostedService<Worker>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.AddServiceDefaults();

builder.Services.AddSingleton<WorkerMetrics>();
builder.Services.AddOpenTelemetry().WithMetrics(m => m.AddMeter(WorkerMetrics.MeterName));

var host = builder.Build();
await host.RunAsync();
