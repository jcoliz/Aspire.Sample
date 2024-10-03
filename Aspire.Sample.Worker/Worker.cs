using System.Runtime.CompilerServices;
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
                await FetchForecastAsync(stoppingToken).ConfigureAwait(false);
                await Task.Delay(weatherOptions.Value.Frequency, stoppingToken);
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
    private async Task FetchForecastAsync(CancellationToken stoppingToken)
    {
        try
        {
            var forecast = await weatherClient.Gridpoint_ForecastAsync(
                weatherOptions.Value.Office, 
                weatherOptions.Value.GridX, 
                weatherOptions.Value.GridY, 
                stoppingToken
            )
            .ConfigureAwait(false);

            var result = forecast?.Properties.Periods;
            if (result is null || result.Count == 0)
            {
                logReceivedMalformed();
            }
            else
            {
                var mapped = mapper.Map<ICollection<GridpointForecastPeriod>, WeatherForecast[]>(result);

                logReceivedOk(mapped.Length);
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
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "{Location}: Received OK {Count} forecasts", EventId = 1010)]
    public partial void logReceivedOk(int count, [CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Error, Message = "{Location}: Received malformed response", EventId = 1018)]
    public partial void logReceivedMalformed([CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Error, Message = "{Location}: Failed", EventId = 1008)]
    public partial void logFail(Exception ex, [CallerMemberName] string? location = null);

    [LoggerMessage(Level = LogLevel.Critical, Message = "{Location}: Critical Failure", EventId = 1009)]
    public partial void logCriticalFail(Exception ex, [CallerMemberName] string? location = null);

}
