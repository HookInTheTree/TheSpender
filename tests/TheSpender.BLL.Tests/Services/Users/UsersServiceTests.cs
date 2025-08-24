using FluentAssertions;
using Moq;
using TheSpender.BLL.Helpers;
using TheSpender.BLL.Services.Users;
using TheSpender.DAL.Entities.Users;
using TheSpender.TestUtils;
using Xunit;

namespace TheSpender.BLL.Tests.Services.Users;

public class UsersServiceTests
{
    private readonly Mock<IStringsHasher> _stringsHasherMock = new();

    [Fact]
    public async Task CreateUser_CorrectInvoke_CreateUser()
    {
        var clientId = "1000000";
        var existedUserClientHash = "some-long-client-hash";
        await using var dbContext = DatabaseFixture.CreateContext();
        var usersService = new UsersService(dbContext, _stringsHasherMock.Object);
        _stringsHasherMock.Setup(x => x.GetHash(clientId)).Returns(existedUserClientHash);

        var user = usersService.CreateUser(clientId, CancellationToken.None);

        user.Id.Should().NotBeEmpty();
        user.ClientId.Should().Be(existedUserClientHash);
    }

    [Fact]
    public async Task GetUserByClientId_UserNotExists_ReturnsNull()
    {
        var clientId = "1000000";
        var hashForClientId = "some-long-client-hash";
        await using var dbContext = DatabaseFixture.CreateContext();

        var usersService = new UsersService(dbContext, _stringsHasherMock.Object);
        _stringsHasherMock.Setup(x => x.GetHash(clientId)).Returns(hashForClientId);

        var user = await usersService.GetUserByClientId(clientId, CancellationToken.None);

        user.Should().Be(null);
    }

    [Fact]
    public async Task GetUserByClientId_UserExists_ReturnsUser()
    {
        var clientId = "1000000";
        var existedUserClientHash = "some-long-client-hash";
        var existedUser = new User { ClientId = existedUserClientHash };

        await using var dbContext = DatabaseFixture.CreateContext();
        dbContext.Users.Add(existedUser);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var usersService = new UsersService(dbContext, _stringsHasherMock.Object);
        _stringsHasherMock.Setup(x => x.GetHash(clientId)).Returns(existedUserClientHash);

        var user = await usersService.GetUserByClientId(clientId, CancellationToken.None);

        user.Should().NotBeNull();
        user.Id.Should().Be(existedUser.Id);
        user.ClientId.Should().Be(existedUserClientHash);
    }
}
