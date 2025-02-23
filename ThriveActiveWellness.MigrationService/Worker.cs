using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace TheiveActiveWellness.MigrationService;

public class Worker(
    IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "Migrations";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        IEnumerable<DbContext> dbContexts = serviceProvider.GetServices<DbContext>();

        foreach (DbContext dbContext in dbContexts)
        {
            using Activity? activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

            try
            {
                activity?.SetTag("DbContext", dbContext.GetType().Name);
                using IServiceScope scope = serviceProvider.CreateScope();

                await EnsureDatabaseAsync(dbContext, stoppingToken);
                await RunMigrationAsync(dbContext, stoppingToken);
                
                activity?.SetStatus(ActivityStatusCode.Ok);
            }
            catch (Exception ex)
            {
                activity?.AddException(ex);
                throw;
            }
        }
        
        hostApplicationLifetime.StopApplication();
    }

    private static async Task EnsureDatabaseAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        IRelationalDatabaseCreator dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Create the database if it does not exist.
            // Do this first so there is then a database to start a transaction against.
            if (!await dbCreator.ExistsAsync(cancellationToken))
            {
                await dbCreator.CreateAsync(cancellationToken);
            }
        });
    }

    private static async Task RunMigrationAsync(DbContext dbContext, CancellationToken cancellationToken)
    {
        IExecutionStrategy strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            // Run migration in a transaction to avoid partial migration if it fails.
            await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);
        });
    }
}
