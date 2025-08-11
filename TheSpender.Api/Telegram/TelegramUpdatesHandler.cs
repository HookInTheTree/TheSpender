using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TheSpender.DAL;

namespace TheSpender.Api.Telegram;

/// <summary>
/// Хендлер сообщений от телеграмма.
/// </summary>
public class TelegramUpdatesHandler(SpenderDbContext dbContext) : IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        await botClient.SendMessage(
            update.Message!.Chat.Id,
            "Привет, у тебя очень красивые глаза",
            cancellationToken: cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
