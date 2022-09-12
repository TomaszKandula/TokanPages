using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Handlers.Queries.Users;
using MediatR;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Users cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class UsersCache : IUsersCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    /// <summary>
    /// Users cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="mediator">Mediator instance</param>
    public UsersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<List<GetAllUsersQueryResult>> GetUsers(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetAllUsersQuery());

        const string key = "users/";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetAllUsersQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllUsersQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetUserQueryResult> GetUser(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetUserQuery { Id = id });

        var key = $"user/{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetUserQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetUserQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}