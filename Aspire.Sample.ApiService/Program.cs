using Aspire.Sample.Application;
using Aspire.Sample.Data;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

// Add services to the container.

builder.Services.AddProblemDetails();
builder.Services.AddDataProvider();
builder.Services.AddApplicationFeatures();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Aspire.Sample Backend";
    options.Description = "Application boundary between .NET backend and frontend.";
});

// https://www.meziantou.net/configuring-json-options-in-asp-net-core.htm
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = false;
    options.SerializerOptions.PropertyNamingPolicy = null;
    options.SerializerOptions.WriteIndented = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapGet("/weatherforecast", (WeatherForecastFeature feature) => feature.ListForecasts());
app.MapPut("/weatherforecast", (WeatherForecastFeature feature) => feature.AddForecast())
    .WithName("AddWeatherForecast");

app.MapDefaultEndpoints();

app.Run();
