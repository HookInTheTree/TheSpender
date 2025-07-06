using Microsoft.Extensions.DependencyInjection;

namespace TheSpender.DAL.Tests;

public static class ServiceCollectionsExtension
{
    public IServiceCollection AddDataAccessLayer(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<SpenderDbContext>();
    }
}
