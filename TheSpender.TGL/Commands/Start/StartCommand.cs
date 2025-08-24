using Telegram.Bot;
using Telegram.Bot.Types;
using TheSpender.BLL.Services.Users;
using TheSpender.DAL;
using TheSpender.TGL.Extensions;

namespace TheSpender.TGL.Commands.Start;

/// <summary>
/// Приветственная команда. Отправляется при получении ботом сообщения /start
/// </summary>
internal sealed class StartCommand(
    SpenderDbContext dbContext,
    IUserService userService,
    ITelegramBotClient botClient) : ICommand
{
    public string CommandName => CommandNames.Start;

    public async Task Execute(Update update, CancellationToken cancellationToken)
    {
        var chatId = update.GetChatId();

        await CreateUser(chatId, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        await botClient.SendMessage(
            chatId,
            HelloMessageText,
            cancellationToken: cancellationToken, replyMarkup: Keyboards.MainMenu);
    }

    private async Task CreateUser(string chatId, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserByClientId(chatId, cancellationToken);

        if (user == null)
        {
            user = userService.CreateUser(chatId, cancellationToken);
            dbContext.Users.Add(user);
        }
    }

    internal const string HelloMessageText = @"
Привет! 👋
Рады видеть тебя в нашем боте.
С его помощью вы сможете легко и быстро вести учёт доходов и расходов

💡 Не откладывай формирование полезной привычки. 
Попробуй записать любую мелочь прямо сейчас (например, `300 кофе`).
Это легко, быстро и запускает большой процесс наведения порядка!";

}
