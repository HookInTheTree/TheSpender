namespace TheSpender.TGL;

/// <summary>
/// Настройки интеграции с телеграмм
/// </summary>
public record TelegramOptions
{
    /// <summary>
    /// Токен телеграм-бота
    /// </summary>
    public required string BotToken { get; init; }

    /// <summary>
    /// Используем ли веб-хуки. Влияет на способ интеграции с телеграммом
    /// </summary>
    public bool UseWebhook { get; init; }

    /// <summary>
    /// Настройки, необходимые при использовании вебхуков от телеграммаю
    /// </summary>
    public WebhookOptions? Webhook { get; init; }

    /// <summary>
    /// Настройки для вебхуков телеграмма
    /// </summary>
    public record WebhookOptions
    {
        /// <summary>
        /// Секретный токен, идентифицирующий подписчика
        /// </summary>
        public required string SecretApiToken { get; init; } = null!;

        /// <summary>
        /// Домен текущего приложения, на которое необходимо настроить хуки
        /// </summary>
        public required string Domain { get; init; } = null!;
    }
}
