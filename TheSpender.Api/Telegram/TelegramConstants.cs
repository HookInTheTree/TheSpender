namespace TheSpender.Api.Telegram;

/// <summary>
/// Константы, необходимые для интеграции с телеграммом
/// </summary>
public static class TelegramConstants
{
    /// <summary>
    /// Заголовок секретного токена, который приходит в веб-хуках от телеграм
    /// </summary>
    public const string SecretTokenAuthHeader = "X-Telegram-Bot-Api-Secret-Token";

    /// <summary>
    /// Роут для обработки запросов при вебхуках
    /// </summary>
    public const string WebhooksHandlingApiRoute = "/api/telegram/webhook";
}
