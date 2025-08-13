using Telegram.Bot;
using Telegram.Bot.Types;
using TheSpender.TGL.Extensions;

namespace TheSpender.TGL.Commands.Unsupported;

internal sealed class UnsupportedCommand(ITelegramBotClient botClient) : ICommand
{
    public string CommandName => CommandNames.UnsupportedCommand;
    public async Task Execute(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.GetChatId();

        await botClient.SendMessage(
            chatId,
            UnsupportedMessageText,
            cancellationToken: cancellationToken, replyMarkup: Keyboards.MainMenu);
    }

    internal const string UnsupportedMessageText = @"Прости, я не смог распознать твою команду или действие.
Скорее возвращаю тебя в основное меню, на случай, если ты заблудился";

}
