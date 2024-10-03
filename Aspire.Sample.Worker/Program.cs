using Aspire.Sample.Worker;
using Aspire.Sample.Worker.Api;
using Aspire.Sample.Worker.Options;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.Configure<WeatherOptions>(
    builder.Configuration.GetSection(WeatherOptions.Section)
);

builder.Services.AddHttpClient<WeatherClient>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
