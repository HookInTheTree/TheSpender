using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheSpender.BLL.Helpers;
using TheSpender.BLL.Services.Users;

namespace TheSpender.BLL;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// Подключает сервисы, реализующие бизнес-логику приложения.
    /// </summary>
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IConfiguration configuration)
    {
        var securityOptions = configuration.GetSection("SecurityOptions");
        if (!securityOptions.Exists())
        {
            throw new InvalidOperationException(@"SecirityOptions configuration section is missing.
                Please ensure 'SecirityOptions' section is defined in your configuration files.");
        }
        services.Configure<SecurityOptions>(securityOptions);
        services.AddSingleton<IStringsHasher, StringsHasher>();
        services.AddScoped<IUserService, UsersService>();

        return services;
    }
}
