using TheSpender.Api.Telegram;

namespace TheSpender.Api;

internal static class ProgramSetup
{
    internal static void AddTelegram(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TelegramOptions>(configuration.GetSection("Telegram"));
        services.AddHostedService<TelegramHostedService>();
    }
}
