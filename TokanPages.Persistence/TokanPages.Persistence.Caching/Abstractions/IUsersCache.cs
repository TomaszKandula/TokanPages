using TokanPages.Backend.Application.Users.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Users cache contract
/// </summary>
public interface IUsersCache
{
    /// <summary>
    /// Returns registered users
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object list</returns>
    Task<List<GetUsersQueryResult>> GetUsers(bool noCache = false);

    /// <summary>
    /// Returns single user data
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false);
}