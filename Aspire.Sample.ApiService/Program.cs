using Aspire.Sample.Data;
using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IDataProvider, ApplicationDbContext>();

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

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (IDataProvider data) =>
{
    var query = data.Get<WeatherForecast>().OrderByDescending(x=>x.Date).Take(10);

    var forecasts = await data.ToListNoTrackingAsync(query);

    return forecasts.ToArray();
});

app.MapPut("/weatherforecast", async (IDataProvider data) =>
{
    var forecast = new WeatherForecast
    (
        0,
        DateOnly.FromDateTime(DateTime.Now),
        Random.Shared.Next(-20, 55),
        summaries[Random.Shared.Next(summaries.Length)]
    );

    data.Add(forecast);
    await data.SaveChangesAsync();
})
.WithName("AddWeatherForecast");

app.MapDefaultEndpoints();

app.Run();
