using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TheSpender.DAL;

/// <summary>
/// Сервис для мигрирования бд, до полного поднятия приложения.
/// Пока процесс миграции не закончится, приложение не стартанёт.
/// </summary>
/// <param name="scopeFactory"></param>
internal sealed class MigrationHostedService(IServiceScopeFactory scopeFactory) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = scopeFactory.CreateScope();

        var logger = scope.ServiceProvider.GetRequiredService<ILogger<MigrationHostedService>>();
        var dbContext = scope.ServiceProvider.GetRequiredService<SpenderDbContext>();

        var migrations = (await dbContext.Database.GetPendingMigrationsAsync(cancellationToken)).ToArray();

        if (migrations is { Length: 0 })
        {
            logger.LogInformation("Migrations. No pending migrations. Database migration ended");
            return;
        }

        logger.LogInformation("Migrations. Database migration processing. Count: {MigrationsCount}. Migrations: {Migrations}", migrations.Length, string.Join("\n\t", migrations));
        await dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
