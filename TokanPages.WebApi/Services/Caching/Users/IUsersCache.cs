namespace TokanPages.WebApi.Services.Caching.Users;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Cqrs.Handlers.Queries.Users;

public interface IUsersCache
{
    Task<IEnumerable<GetAllUsersQueryResult>> GetUsers(bool noCache = false);

    Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false);
}