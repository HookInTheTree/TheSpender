namespace TheSpender.TGL.Commands;

/// <summary>
/// Системные названия команд
/// </summary>
internal static class CommandNames
{
    /// <summary>
    /// Команда старта взаимодействия с пользователем
    /// </summary>
    internal const string Start = "/start";

    /// <summary>
    /// Команда получения операций пользователя
    /// </summary>
    internal const string MyOperations = "/my_operations";

    /// <summary>
    /// Команда получения разницы между доходами и расходами в операциях
    /// </summary>
    internal const string MyBalance = "/my_balance";

    /// <summary>
    /// Команда получения категорий пользователя
    /// </summary>
    internal const string MyCategories = "/my_categories";

    /// <summary>
    /// Команда удаления операции
    /// </summary>
    internal const string DeleteOperation = "/delete_operation";

    /// <summary>
    /// Команда при неизвестном действии пользователя
    /// </summary>
    internal const string UnsupportedCommand = "/unsupported_command";

    /// <summary>
    /// Команды, данные которых возвращаются в Update.Message
    /// </summary>
    internal static readonly IReadOnlyCollection<string> Messages = new[]
    {
        Start
    };
}
