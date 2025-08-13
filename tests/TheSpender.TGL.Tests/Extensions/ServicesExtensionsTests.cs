using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using TheSpender.TGL.Commands;
using TheSpender.TGL.Extensions;
using Xunit;

namespace TheSpender.TGL.Tests.Extensions;

public class ServicesExtensionsTests
{
    [Fact]
    public void AddTelegramLayer_CorrectConfiguration_ServiceCollectionConfiguredCorrectly()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(
            new Dictionary<string, string?>()
        {
            ["Telegram:BotToken"] = "Don't put it there! Use secrets.json",
            ["Telegram:UseWebhook"] = "true",
            ["Telegram:Webhook:Domain"] = "suitably-autonomous-poodle.cloudpub.ru",
            ["Telegram:Webhook:SecretApiToken"] = "Don't put it there! Use secrets.json"
        }).Build();

        services.AddTelegramLayer(configuration);

        services.Should().NotBeEmpty();

        var hostedService = services.SingleOrDefault(x => x.ImplementationType == typeof(TelegramInitializingHostedService));
        hostedService.Should().NotBeNull();
        hostedService.Lifetime.Should().Be(ServiceLifetime.Singleton);

        var updatesHandler = services.SingleOrDefault(x => x.ServiceType == typeof(IUpdateHandler));
        updatesHandler.Should().NotBeNull();
        updatesHandler.Lifetime.Should().Be(ServiceLifetime.Scoped);

        var tgBotClient = services.SingleOrDefault(x => x.ServiceType == typeof(ITelegramBotClient));
        tgBotClient.Should().NotBeNull();
        tgBotClient.Lifetime.Should().Be(ServiceLifetime.Transient);

        var commandType = typeof(ICommand);
        var expectedCommands = commandType.Assembly.GetTypes()
            .Where(x => x.IsClass && !x.IsAbstract && commandType.IsAssignableFrom(x))
            .Select(x => x.FullName)
            .ToArray();

        var actualCommands = services.Where(x => x.ServiceType == commandType).ToArray();
        actualCommands.Should().NotBeEmpty();
        actualCommands.Length.Should().Be(expectedCommands.Length);
        actualCommands.Should().AllSatisfy(x => expectedCommands.Contains(x.ImplementationType.FullName));
    }

    [Fact]
    public void AddTelegramLayer_IncorrectConfiguration_ThrowsInvalidOperationException()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        services.Invoking(x => x.AddTelegramLayer(configuration))
            .Should()
            .Throw<InvalidOperationException>();
    }
}
