using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Users cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class UsersCache : IUsersCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Users cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    public UsersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<List<GetAllUsersQueryResult>> GetUsers(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetAllUsersQuery());

        var key = $"{_environment.EnvironmentName}:users";
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

        var key = $"{_environment.EnvironmentName}:user:{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetUserQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetUserQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}