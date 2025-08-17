using Telegram.Bot;
using Telegram.Bot.Types;
using TheSpender.TGL.Extensions;

namespace TheSpender.TGL.Commands.Start;

/// <summary>
/// Приветственная команда. Отправляется при получении ботом сообщения /start
/// </summary>
internal sealed class StartCommand(ITelegramBotClient botClient) : ICommand
{
    public string CommandName => CommandNames.Start;

    public async Task Execute(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.GetChatId();

        await botClient.SendMessage(
            chatId,
            HelloMessageText,
            cancellationToken: cancellationToken, replyMarkup: Keyboards.MainMenu);
    }

    internal const string HelloMessageText = @"
Привет! 👋
Рады видеть тебя в нашем боте.
С его помощью вы сможете легко и быстро вести учёт доходов и расходов

💡 Не откладывай формирование полезной привычки. 
Попробуй записать любую мелочь прямо сейчас (например, `300 кофе`).
Это легко, быстро и запускает большой процесс наведения порядка!";

}
