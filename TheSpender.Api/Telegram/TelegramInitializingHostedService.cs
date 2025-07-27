using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TheSpender.Api.Telegram;

/// <summary>
/// Инициализирует интеграцию с телеграммом при запуске приложения.
/// </summary>
public class TelegramInitializingHostedService(
    ITelegramBotClient telegramBot,
    IUpdateHandler updateHandler,
    IOptions<TelegramOptions> options,
    ILogger<TelegramInitializingHostedService> logger) : IHostedService
{
    private readonly TelegramOptions _telegramOptions = options.Value;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting Telegram hosted service to set up telegram updates handling way. Bot updates handling way: [{HandlingWay}]",
            _telegramOptions.UseWebhook ? "webhooks" : "long polling");

        if (_telegramOptions.UseWebhook)
        {
            SetWebHook(cancellationToken);
        }
        else
        {
            SetLongPolling(cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting Telegram hosted service to set up telegram updates handling way. Bot updates handling way: [{HandlingWay}]",
            _telegramOptions.UseWebhook ? "webhooks" : "long polling");

        if (_telegramOptions.UseWebhook)
        {
            RemoveWebHook(cancellationToken);
        }

        return Task.CompletedTask;
    }

    private void SetLongPolling(CancellationToken cancellationToken) =>
        telegramBot.StartReceiving(
            updateHandler: updateHandler,
            cancellationToken: cancellationToken
        );

    private void SetWebHook(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void RemoveWebHook(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }


}
