using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TheSpender.TGL.Commands;
using static TheSpender.TGL.Extensions.TelegramUpdateExtensions;

namespace TheSpender.TGL;

/// <summary>
/// Хендлер сообщений от телеграмма.
/// </summary>
internal sealed class TelegramUpdatesHandler(IEnumerable<ICommand> commands, ILogger<TelegramUpdatesHandler> logger) : IUpdateHandler
{
    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var commandName = update.ExtractCommandName();

        var command = commands.SingleOrDefault(x => x.CommandName == commandName);

        if (command == null)
        {
            var unsupportedCommand = commands.Single(x => x.CommandName == CommandNames.UnsupportedCommand);
            await unsupportedCommand.Execute(update, cancellationToken);

            return;
        }

        await command.Execute(update, cancellationToken);
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, HandleErrorSource source,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Some exception was thrown. Error source {Source}", source);
        return Task.CompletedTask;
    }
}
