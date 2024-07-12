using Aspire.Sample.Data;
using Microsoft.EntityFrameworkCore;

namespace Aspire.Sample.MigrationService;

// Inpsired by: https://github.com/dotnet/aspire-samples/tree/main/samples/DatabaseMigrations/DatabaseMigrations.MigrationService

public partial class Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IHostApplicationLifetime lifetime) : BackgroundService
{
    public readonly ILogger _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await RunMigrationAsync(dbContext, stoppingToken);

            LogOk();
        }
        catch (Exception ex)
        {
            LogFail(ex);
        }

        lifetime.StopApplication();
    }

    private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }

    [LoggerMessage(Level = LogLevel.Information, Message = "Database migration: OK")]
    public partial void LogOk();

    [LoggerMessage(Level = LogLevel.Critical, Message = "Database migration: Failed")]
    public partial void LogFail(Exception ex);
}
