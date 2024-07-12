using Aspire.Sample.Data;
using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.AddNpgsqlDbContext<ApplicationDbContext>("forecasts");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IDataProvider, ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

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

app.MapDefaultEndpoints();

//var scope = app.Services.CreateScope();

//var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

// Postgres databases are always brought current by the application,
// because they are only ever used for development and testing.
//
// SqlServer databases must be created by EF Core tooling or SQL scripts!

//db.Database.Migrate();

app.Run();
