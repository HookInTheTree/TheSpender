using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TheSpender.TGL.Commands;

namespace TheSpender.TGL.Extensions;

/// <summary>
/// Расширения для объекта обновления от телеграмм
/// </summary>
internal static class TelegramUpdateExtensions
{
    /// <summary>
    /// Распознавание названия команды, переданной от пользователя
    /// </summary>
    internal static string? ExtractCommandName(this Update update) =>
        update.Type switch
        {
            UpdateType.Message when CommandNames.Messages.Contains(update.Message!.Text) =>
                update.Message.Text,
            UpdateType.CallbackQuery => update.CallbackQuery!.Data,
            _ => null
        };

    /// <summary>
    /// Получение идентификатора чата из объекта обновления
    /// </summary>
    internal static string GetChatId(this Update update) => update.Type switch
    {
        UpdateType.Message => update.Message!.Chat.Id.ToString(),
        UpdateType.CallbackQuery => update.CallbackQuery!.Message!.Chat!.Id.ToString(),
        _ => throw new InvalidOperationException($"Operation with type {update.Type} is not supported")
    };
}
