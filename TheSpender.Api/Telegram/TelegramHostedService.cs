using Microsoft.Extensions.Options;

namespace TheSpender.Api.Telegram;

public class TelegramHostedService(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<TelegramOptions> options,
    ILogger<TelegramHostedService> logger) : IHostedService
{
    private readonly TelegramOptions _telegramOptions = options.Value;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        string handlingWay = _telegramOptions.UseWebhook
            ? "webhooks"
            : "long polling";

        logger.LogInformation(
            "Starting Telegram hosted service to set up telegram updates handling way. Bot updates handling way: [{handlingWay}]",
            handlingWay);

        if (_telegramOptions.UseWebhook)
        {
            SetWebHooks(cancellationToken);
        }
        else
        {
            SetLongPolling(cancellationToken);
        }

        logger.LogInformation("Telegram updates handling way: {handlingWay} was setted.");

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        if (_telegramOptions.UseWebhook)
        {
            RemoveWebHooks(cancellationToken);
        }
        else
        {
            RemoveLongPolling(cancellationToken);
        }

        return Task.CompletedTask;
    }

    private void SetLongPolling(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void SetWebHooks(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void RemoveWebHooks(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private void RemoveLongPolling(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
