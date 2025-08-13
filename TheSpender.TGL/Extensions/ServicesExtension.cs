using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TheSpender.TGL.Commands;

namespace TheSpender.TGL.Extensions;

public static class ServicesExtension
{
    /// <summary>
    /// Регистрируем логику взаимодействия с телеграммом
    /// </summary>
    public static IServiceCollection AddTelegramLayer(this IServiceCollection services, IConfiguration configuration)
    {
        AddTelegramIntegrationServices(services, configuration);
        AddCommands(services);
        return services;
    }

    /// <summary>
    /// Добавляет сервисы, необходимые для интеграции с телеграмм
    /// </summary>
    private static void AddTelegramIntegrationServices(IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection("Telegram");

        if (!settings.Exists())
        {
            throw new InvalidOperationException(@"Telegram configuration section is missing.
                Please ensure 'Telegram' section is defined in your configuration files.");
        }

        services.Configure<TelegramOptions>(settings);
        services.AddHttpClient("telegramBotClient").AddTypedClient<ITelegramBotClient>(
            (client, provider) => {
                var options = provider.GetRequiredService<IOptions<TelegramOptions>>().Value;
                return new TelegramBotClient(options.BotToken, client);
            });
        services.AddScoped<IUpdateHandler, TelegramUpdatesHandler>();
        services.AddHostedService<TelegramInitializingHostedService>();
    }

    /// <summary>
    /// Регистрация команд
    /// </summary>
    private static void AddCommands(IServiceCollection serviceCollection)
    {
        var commandsInterfaceType = typeof(ICommand);

        var commandTypes = commandsInterfaceType.Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && commandsInterfaceType.IsAssignableFrom(x)).ToArray();

        foreach (var commandType in commandTypes)
        {
            serviceCollection.Add(new ServiceDescriptor(commandsInterfaceType, commandType, ServiceLifetime.Scoped));
        }
    }
}
