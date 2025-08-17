using TheSpender.DAL.Entities.Users;

namespace TheSpender.BLL.Services.Users;

/// <summary>
/// Контракт бизнес-сервиса пользователей
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Метод для создания пользователя по внешнему идентификатору.
    /// Пользователь создаётся только в том случае, если он не был найден по внешнему идентификатору
    /// </summary>
    /// <param name="clientId">Внешний идентификатор пользователя</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Пользователь</returns>
    public Task<User> CreateUser(string clientId, CancellationToken cancellationToken);

    /// <summary>
    /// Получение пользователя по внешнему идентификатору
    /// </summary>
    /// <param name="clientId">Внешний идентификатор пользователя</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Пользователь</returns>
    public Task<User?> GetUserByClientId(string clientId, CancellationToken cancellationToken);
}
