using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TheSpender.Api.Telegram;
using TheSpender.DAL;

namespace TheSpender.Api;

/// <summary>
/// Логически разнесённые конфигурации приложения
/// </summary>
internal static class ProgramSetup
{
    /// <summary>
    /// Подрубаем телеграм
    /// </summary>
    internal static void AddTelegram(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
        services.AddHttpClient("telegramBotClient").AddTypedClient<ITelegramBotClient>(
            (client, provider) => {
                var options = provider.GetRequiredService<IOptions<TelegramOptions>>().Value;
                return new TelegramBotClient(options.BotToken, client);
        });
        services.AddScoped<IUpdateHandler, TelegramUpdatesHandler>();
        services.AddHostedService<TelegramInitializingHostedService>();
    }

    /// <summary>
    /// Подрубаем базу данных
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    internal static void AddDatabase(IServiceCollection services, IConfiguration configuration) =>
        services.AddDataAccessLayer(configuration);
}
