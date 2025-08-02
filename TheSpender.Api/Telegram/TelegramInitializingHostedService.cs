using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TheSpender.Api.Telegram;

/// <summary>
/// Инициализирует интеграцию с телеграммом при запуске приложения.
/// Пока процесс не закончится, приложение не стартанёт.
/// </summary>
public class TelegramInitializingHostedService(
    ITelegramBotClient telegramBot,
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
            SetLongPolling(cancellationToken);
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

    private void SetLongPolling(CancellationToken cancellationToken) =>
        telegramBot.StartReceiving(
            updateHandler: TelegramUpdatesHandler.HandleUpdateAsync,
            errorHandler: TelegramUpdatesHandler.HandleErrorAsync,
            cancellationToken: cancellationToken
        );

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
