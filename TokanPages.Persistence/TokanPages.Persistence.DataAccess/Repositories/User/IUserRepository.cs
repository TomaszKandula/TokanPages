using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public interface IUserRepository
{
    /// <summary>
    /// Returns user entity by the given ID.
    /// </summary>
    /// <remarks>
    /// ID is a primary key of an entity.
    /// </remarks>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user data, otherwise null.</returns>
    Task<Users.User?> GetUserById(Guid userId);

    /// <summary>
    /// Returns user information entity by given ID.
    /// </summary>
    /// <param name="userId">A mandatory user ID.</param>
    /// <returns>If found, returns user information, otherwise null.</returns>
    Task<Users.UserInfo?> GetUserInformationById(Guid userId);
}