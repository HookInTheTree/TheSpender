using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using TheSpender.Api.Telegram;

namespace TheSpender.Api.Controllers;

[ApiController]
[Route(TelegramConstants.WebhooksHandlingApiRoute)]
public class TelegramBotController(ITelegramBotClient telegramBot, IUpdateHandler updateHandler, IOptions<TelegramOptions> options) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> HandleUpdate([FromBody] Update update, CancellationToken token)
    {
        if (Request.Headers[TelegramConstants.SecretTokenAuthHeader] != options.Value.Webhook!.SecretApiToken)
        {
            return Unauthorized();
        }

        await updateHandler.HandleUpdateAsync(telegramBot, update, token);
        return Ok();
    }
}
