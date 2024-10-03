using System.Runtime.CompilerServices;
using Aspire.Sample.Application;
using Aspire.Sample.Models;
using Aspire.Sample.Worker.Api;
using Aspire.Sample.Worker.Options;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace Aspire.Sample.Worker;

public partial class Worker(
    WeatherClient weatherClient, 
    IOptions<WeatherOptions> weatherOptions, 
    IMapper mapper,
    IServiceProvider services,
    ILogger<Worker> logger
    ) : BackgroundService
{
    private readonly ILogger<Worker> _logger = logger;

    /// <summary>
    /// Main loop which continually fetches readings, then sends to Log Analytics
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var forecasts = await FetchForecastsAsync(stoppingToken).ConfigureAwait(false);
                if (forecasts != null)
                {
                    await StoreForecastsAsync(forecasts).ConfigureAwait(false);
                }

                await Task.Delay(weatherOptions.Value.Frequency, stoppingToken).ConfigureAwait(false);
            }
        }
        catch (TaskCanceledException)
        {
            // Task cancellation is not an error, no action required
        }
        catch (Exception ex)
        {
            logCriticalFail(ex);
        }
    }

    /// <summary>
    /// Fetch forecast from Weather Service
    /// </summary>
    /// <param name="stoppingToken">Cancellation token</param>
    private async Task<ICollection<GridpointForecastPeriod>?> FetchForecastsAsync(CancellationToken stoppingToken)
    {
        ICollection<GridpointForecastPeriod>? result = null;

        try
        {
            var forecast = await weatherClient.Gridpoint_ForecastAsync(
                weatherOptions.Value.Office, 
                weatherOptions.Value.GridX, 
                weatherOptions.Value.GridY, 
                stoppingToken
            )
            .ConfigureAwait(false);

            result = forecast?.Properties.Periods;
            if (result is null || result.Count == 0)
            {
                logReceivedMalformed();
            }
            else
            {
                logReceivedOk(result.Count);
            }
        }
        catch (TaskCanceledException)
        {
            // Task cancellation is not an error, no action required
        }
        catch (Exception ex)
        {
            logFail(ex);
        }

        return result;
    }

    private async Task StoreForecastsAsync(ICollection<GridpointForecastPeriod> forecasts)
    {
        try
        {
            // We want a fresh scope every time we come through here.
            var scope = services.CreateScope();
            var feature = scope.ServiceProvider.GetRequiredService<WeatherForecastFeature>();

            // Map the received forecasts into the form we store in the database
            var mapped = mapper.Map<ICollection<GridpointForecastPeriod>, WeatherForecast[]>(forecasts);

            // Send them to application logic to be updated
            (var updated, var added) = await feature.UpdateForecasts(mapped).ConfigureAwait(false);

            logStoredOk(updated, added);
        }
        catch (TaskCanceledException)
        {
            // Task cancellation is not an error, no action required
        }
        catch (Exception ex)
        {
            logFail(ex);
        }
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "{Location}: Received OK {Count} forecasts", EventId = 1010)]
    public partial void logReceivedOk(int count, [CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Error, Message = "{Location}: Received malformed response", EventId = 1018)]
    public partial void logReceivedMalformed([CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Information, Message = "{Location}: Stored OK {CountUpdated} updated {CountAdded} added", EventId = 1020)]
    public partial void logStoredOk(int countUpdated, int countAdded, [CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Error, Message = "{Location}: Failed", EventId = 1008)]
    public partial void logFail(Exception ex, [CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Critical, Message = "{Location}: Critical Failure", EventId = 1009)]
    public partial void logCriticalFail(Exception ex, [CallerMemberName] string? location = null);

}
