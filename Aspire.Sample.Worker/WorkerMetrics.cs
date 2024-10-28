using System.Diagnostics.Metrics;

namespace Aspire.Sample.Worker;

// https://learn.microsoft.com/en-us/dotnet/core/diagnostics/metrics-instrumentation#get-a-meter-via-dependency-injection

public class WorkerMetrics
{
    private readonly Counter<int> _connectionsOk;
    private readonly Counter<int> _connectionsMalformed;
    private readonly Counter<int> _connectionsFailed;
    private readonly Counter<int> _forecastsReceived;
    private readonly Counter<int> _forecastsAdded;
    private readonly Counter<int> _forecastsStorageFailed;

    public static string MeterName => "Aspire.Sample";

    public WorkerMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _connectionsOk = meter.CreateCounter<int>("worker.connections.ok");
        _connectionsFailed = meter.CreateCounter<int>("worker.connections.failed");
        _connectionsMalformed = meter.CreateCounter<int>("worker.connections.malformed");
        _forecastsReceived = meter.CreateCounter<int>("worker.forecasts.received");
        _forecastsAdded = meter.CreateCounter<int>("worker.forecasts.added");
        _forecastsStorageFailed = meter.CreateCounter<int>("worker.forecasts.storage-failed");
    }

    public void ConnectionOk()
    {
        _connectionsOk.Add(1);
    }

    public void ConnectionFailed()
    {
        _connectionsFailed.Add(1);        
    }

    public void ConnectionMalformed()
    {
        _connectionsMalformed.Add(1);        
    }    


    public void ForecastsReceived(int count)
    {
        _forecastsReceived.Add(count);
    }


    public void ForecastsAdded(int count)
    {
        _forecastsAdded.Add(count);
    }

    public void ForecastStorageFailed()
    {
        _forecastsStorageFailed.Add(1);

    }
}
