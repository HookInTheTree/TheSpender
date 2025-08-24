namespace TheSpender.BLL;

/// <summary>
/// Настройки, необходимые для работы с пользователями
/// </summary>
internal sealed record SecurityOptions
{
    /// <summary>
    /// Соль, необходимая для хэширования внешних идентификаторов пользователей
    /// </summary>
    internal required string Salt { get; set; }
}
