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
    public async Task<User> CreateUser(string clientId, CancellationToken cancellationToken)
    {
        var clientHash = hasher.GetHash(clientId);
        var user = await GetUserByClientHash(clientHash, cancellationToken);

        if (user == null)
        {
            user = new User()
            {
                ClientId = clientHash,
            };

            dbContext.Users.Add(user);
        }
        await dbContext.SaveChangesAsync(cancellationToken);

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
