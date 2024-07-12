using Aspire.Sample.Data;

namespace Aspire.Sample.MigrationService;

public partial class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider) : BackgroundService
{
    public readonly ILogger _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var ok = dbContext.Database.CanConnect();
                LogHeartbeat(ok);
            }
            await Task.Delay(1000, stoppingToken);
        }
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Database status: {CanConnect}")]
    public partial void LogHeartbeat(bool canConnect);
}
