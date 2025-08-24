using Microsoft.EntityFrameworkCore;
using TheSpender.BLL.Helpers;
using TheSpender.DAL;
using TheSpender.DAL.Entities.Users;

namespace TheSpender.BLL.Services.Users;

/// <inheritdoc cref="IUserService"/>
internal sealed class UsersService(
    SpenderDbContext dbContext,
    IStringsHasher hasher) : IUserService
{
    public User CreateUser(string clientId, CancellationToken cancellationToken)
    {
        var clientHash = hasher.GetHash(clientId);
        var user = new User()
        {
            ClientId = clientHash,
        };

        return user;
    }

    public Task<User?> GetUserByClientId(string clientId, CancellationToken cancellationToken)
    {
        var clientHash = hasher.GetHash(clientId);
        return GetUserByClientHash(clientHash, cancellationToken);
    }

    private async Task<User?> GetUserByClientHash(string clientHash, CancellationToken cancellationToken) => //TODO: добавить includs, когда заедут навигационные свойства
        await dbContext.Users.SingleOrDefaultAsync(x => x.ClientId == clientHash, cancellationToken);

}
