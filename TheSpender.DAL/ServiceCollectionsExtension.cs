using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace TheSpender.DAL;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Подключает базу данных, а также способ её миграции
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddDataAccessLayer(this  IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<SpenderDbContext>(builder =>
            builder.UseNpgsql(configuration.GetConnectionString(nameof(NpgsqlConnection))));

        serviceCollection.AddHostedService<MigrationHostedService>();

        return serviceCollection;
    }
}
