using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TheSpender.BLL.Helpers;
using TheSpender.BLL.Services.Users;
using Xunit;

namespace TheSpender.BLL.Tests;

public class ServiceCollectionsExtensionsTests
{
    [Fact]
    public void AddBusinessLogic_CorrectConfiguration_ServiceCollectionConfiguredCorrectly()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().AddInMemoryCollection(
            new Dictionary<string, string?>()
        {
            ["SecurityOptions:Salt"] = "Don't put it there! Use secrets.json",
        }).Build();

        services.AddBusinessLogic(configuration);
        services.Should().NotBeEmpty();

        var usersService = services.SingleOrDefault(x => x.ServiceType == typeof(IUserService));
        usersService.Should().NotBeNull();
        usersService.Lifetime.Should().Be(ServiceLifetime.Scoped);

        var stringsHasherService = services.SingleOrDefault(x => x.ServiceType == typeof(IStringsHasher));
        stringsHasherService.Should().NotBeNull();
        stringsHasherService.Lifetime.Should().Be(ServiceLifetime.Singleton);

        var securityOptions = services.SingleOrDefault(x => x.ServiceType == typeof(IConfigureOptions<SecurityOptions>));
        securityOptions.Should().NotBeNull();
        securityOptions.Lifetime.Should().Be(ServiceLifetime.Singleton);
    }

    [Fact]
    public void AddBusinessLogic_IncorrectConfiguration_ThrowsInvalidOperationException()
    {
        var services = new ServiceCollection();
        var configuration = new ConfigurationBuilder().Build();

        services.Invoking(x => x.AddBusinessLogic(configuration))
            .Should()
            .Throw<InvalidOperationException>();
    }
}
