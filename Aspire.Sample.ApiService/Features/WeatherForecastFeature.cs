using Aspire.Sample.Models;
using Aspire.Sample.Providers;
using System.Diagnostics;
using OpenTelemetry.Trace;

namespace Aspire.Sample.ApiService.Features;

/// <summary>
/// Application logic to manage weathewr forecasts
/// </summary>
/// <param name="dataProvider">Where to retrieve/store data</param>
public class WeatherForecastFeature(IDataProvider dataProvider)
{
    private static readonly ActivitySource _activitySource = new("ApiService.Features.WeatherForecastFeature");

    private static readonly string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    /// <summary>
    /// List recent forecasts
    /// </summary>
    /// <returns>Recent forecasts</returns>
    public async Task<WeatherForecast[]> ListForecasts()
    {
        using var activity = _activitySource.StartActivity(nameof(ListForecasts), ActivityKind.Server);

        try
        {
            var query = dataProvider.Get<WeatherForecast>().OrderByDescending(x => x.Date).Take(10);

            var forecasts = await dataProvider.ToListNoTrackingAsync(query);

            return forecasts.ToArray();
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }
    }

    /// <summary>
    /// Add a forecast
    /// </summary>
    public async Task AddForecast()
    {
        using var activity = _activitySource.StartActivity(nameof(AddForecast), ActivityKind.Server);

        try
        {
            var forecast = new WeatherForecast
            (
                0,
                DateOnly.FromDateTime(DateTime.Now),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            );

            dataProvider.Add(forecast);
            await dataProvider.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            activity?.RecordException(ex);
            throw;
        }
    }
}
