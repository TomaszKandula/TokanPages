namespace TokanPages.Persistence.Caching.Abstractions;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Application.Handlers.Queries.Users;

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
    Task<List<GetAllUsersQueryResult>> GetUsers(bool noCache = false);

    /// <summary>
    /// Returns single user data
    /// </summary>
    /// <param name="id">User ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false);
}