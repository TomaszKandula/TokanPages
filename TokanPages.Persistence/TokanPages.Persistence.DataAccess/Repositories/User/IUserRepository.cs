using TokanPages.Persistence.DataAccess.Repositories.User.Models;
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

    /// <summary>
    /// Returns list of assigned user roles for the given user ID.
    /// </summary>
    /// <param name="userId">User ID.</param>
    /// <returns></returns>
    Task<List<GetUserRolesDto>> GetUserRoles(Guid userId);

    /// <summary>
    /// Creates HTTP request information for the given IP address and requested handler name.
    /// </summary>
    /// <param name="ipAddress">IP address (referer).</param>
    /// <param name="handlerName">Requested handler name (CQRS).</param>
    Task InsertHttpRequest(string ipAddress, string handlerName);
}