using Telegram.Bot.Types;

namespace TheSpender.TGL.Commands;

/// <summary>
/// Контракт телеграмм-команды
/// </summary>
internal interface ICommand
{
    /// <summary>
    /// Системной имя команды. Используется для распознавания
    /// </summary>
    string CommandName { get; }

    /// <summary>
    /// Логика обработки команды
    /// </summary>
    Task Execute(Update update, CancellationToken cancellationToken);
}
