namespace TheSpender.Api.Telegram;

/// <summary>
/// Настройки интеграции с телеграмм
/// </summary>
public class TelegramOptions
{
    /// <summary>
    /// Токен телеграм-бота
    /// </summary>
    public string BotToken { get; init; }

    /// <summary>
    /// Используем ли веб-хуки. Влияет на способ интеграции с телеграммом
    /// </summary>
    public bool UseWebhook { get; init; }
}
