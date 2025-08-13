using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TheSpender.TGL;

/// <summary>
/// Инициализирует интеграцию с телеграммом при запуске приложения.
/// Пока процесс не закончится, приложение не стартанёт.
/// </summary>
internal sealed class TelegramInitializingHostedService(
    ITelegramBotClient telegramBot,
    IServiceProvider serviceProvider,
    IOptions<TelegramOptions> options,
    ILogger<TelegramInitializingHostedService> logger) : IHostedService
{
    private readonly TelegramOptions _telegramOptions = options.Value;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting Telegram hosted service to set up telegram updates handling way. Bot updates handling way: [{HandlingWay}]",
            _telegramOptions.UseWebhook ? "webhooks" : "long polling");

        if (_telegramOptions.UseWebhook)
        {
            await SetWebHook(cancellationToken);
        }
        else
        {
            await SetLongPolling(cancellationToken);
        }

        logger.LogInformation("Telegram updates handling way successfully setted");
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping the app. Removing telegram updates handling way.");

        if (_telegramOptions.UseWebhook)
        {
            await RemoveWebHook(cancellationToken);
        }
    }

    private async Task SetLongPolling(CancellationToken cancellationToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();
        var updatesHandler = scope.ServiceProvider.GetRequiredService<IUpdateHandler>();

        telegramBot.StartReceiving(
            updateHandler: updatesHandler,
            cancellationToken: cancellationToken
        );
    }

    private Task SetWebHook(CancellationToken cancellationToken)
    {
        var webnookUrl = $"https://{_telegramOptions.Webhook!.Domain}{TelegramConstants.WebhooksHandlingApiRoute}";

        return telegramBot.SetWebhook(webnookUrl,
            secretToken: _telegramOptions.Webhook.SecretApiToken,
            cancellationToken: cancellationToken);
    }

    private Task RemoveWebHook(CancellationToken cancellationToken) =>
        telegramBot.DeleteWebhook(false,
            cancellationToken);
}
