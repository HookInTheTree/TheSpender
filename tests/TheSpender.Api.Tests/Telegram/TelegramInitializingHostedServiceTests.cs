using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using TheSpender.Api.Telegram;
using Xunit;

namespace TheSpender.Api.Tests.Telegram;

public class TelegramInitializingHostedServiceTests
{
    private readonly Mock<ITelegramBotClient> _tgBotClientMock = new();
    private readonly Mock<ILogger<TelegramInitializingHostedService>> _loggerMock = new();
    private readonly Mock<IServiceProvider> _serviceProviderMock = new();
    private readonly Mock<IServiceScopeFactory> _scopeFactoryMock = new();
    private readonly Mock<IUpdateHandler> _updateHandler = new();

    public TelegramInitializingHostedServiceTests()
    {
        var scope = new Mock<IServiceScope>();
        scope.Setup(x => x.ServiceProvider).Returns(_serviceProviderMock.Object);

        _scopeFactoryMock.Setup(x => x.CreateScope()).Returns(scope.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IServiceScopeFactory))).Returns(_scopeFactoryMock.Object);
        _serviceProviderMock.Setup(x => x.GetService(typeof(IUpdateHandler))).Returns(_updateHandler.Object);
    }

    [Fact]
    public async Task StartAsync_WebhooksEnabled_SetWebhooksRequestInvokedCorrectly()
    {
        var expectedWebhooksUrl = "https://test.com/api/telegram/webhook";
        var options = new TelegramOptions()
        {
            UseWebhook = true,
            Webhook = new TelegramOptions.WebhookOptions()
            {
                Domain = "test.com",
                SecretApiToken = "test-secret-api-token"
            },
            BotToken = Guid.NewGuid().ToString()
        };
        var hostedService = new TelegramInitializingHostedService(
            _tgBotClientMock.Object,
            _serviceProviderMock.Object,
            Options.Create(options),
            _loggerMock.Object);

        await hostedService.StartAsync(CancellationToken.None);

        _tgBotClientMock.Verify(x => x.SendRequest(It.Is<SetWebhookRequest>(x => x.Url == expectedWebhooksUrl &&
                x.Certificate == null &&
                x.IpAddress == null &&
                x.AllowedUpdates == null &&
                x.MaxConnections == null &&
                x.DropPendingUpdates == false &&
                x.SecretToken == options.Webhook.SecretApiToken), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task StartAsync_WebhooksDisabled_LongPollingInvokedCorrectly()
    {
        var options = new TelegramOptions()
        {
            UseWebhook = false,
            Webhook = null,
            BotToken = Guid.NewGuid().ToString()
        };

        var hostedService = new TelegramInitializingHostedService(
            _tgBotClientMock.Object,
            _serviceProviderMock.Object,
            Options.Create(options),
            _loggerMock.Object);

        await hostedService.StartAsync(CancellationToken.None);

        _tgBotClientMock.Verify(x => x.SendRequest(It.IsAny<GetUpdatesRequest>(), It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task StopAsync_WebhooksEnabled_WebhooksSuccessfullyDeleted()
    {
        var options = new TelegramOptions()
        {
            UseWebhook = true,
            Webhook = new TelegramOptions.WebhookOptions()
            {
                Domain = "test.com",
                SecretApiToken = "test-secret-api-token"
            },
            BotToken = Guid.NewGuid().ToString()
        };

        var hostedService = new TelegramInitializingHostedService(
            _tgBotClientMock.Object,
            _serviceProviderMock.Object,
            Options.Create(options),
            _loggerMock.Object);

        await hostedService.StopAsync(CancellationToken.None);

        _tgBotClientMock.Verify(x => x.SendRequest(
            It.Is<DeleteWebhookRequest>(x => x.DropPendingUpdates == false),
            It.IsAny<CancellationToken>()));
    }

    [Fact]
    public async Task StopAsync_WebhooksDisabled_WeDontTryDeleteWebhooks()
    {
        var options = new TelegramOptions()
        {
            UseWebhook = false,
            Webhook = null,
            BotToken = Guid.NewGuid().ToString()
        };

        var hostedService = new TelegramInitializingHostedService(
            _tgBotClientMock.Object,
            _serviceProviderMock.Object,
            Options.Create(options),
            _loggerMock.Object);

        await hostedService.StopAsync(CancellationToken.None);

        _tgBotClientMock.VerifyNoOtherCalls();
    }
}
