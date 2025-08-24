using Telegram.Bot.Types.ReplyMarkups;

namespace TheSpender.TGL.Commands;

/// <summary>
/// Наиболее часто используемые клавиатуры с кнопками для пользователя
/// </summary>
internal static class Keyboards
{
    /// <summary>
    /// Основное меню бота, которое отображается в чате с пользователем.
    /// Меню отображается в виде сообщения от бота.
    /// </summary>
    internal static readonly InlineKeyboardMarkup MainMenu = new()
    {
        InlineKeyboard =
        [
            [InlineKeyboardButton.WithCallbackData("\ud83e\uddee Баланс", CommandNames.MyBalance)],
            [InlineKeyboardButton.WithCallbackData("\ud83d\udccb Мои операции", CommandNames.MyOperations)],
            [InlineKeyboardButton.WithCallbackData("\ud83d\udcc2 Мои категории", CommandNames.MyCategories)],
            [InlineKeyboardButton.WithCallbackData("\u2796 Удалить операцию", CommandNames.DeleteOperation)]
        ],
    };
}
