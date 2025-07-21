using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Users cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class UsersCache : IUsersCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IUserService _userService;

    private readonly IMediator _mediator;

    /// <summary>
    /// Users cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    /// <param name="userService">User service instance</param>
    public UsersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, 
        IHostEnvironment environment, IUserService userService)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<List<GetUsersQueryResult>> GetUsers(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetUsersQuery());

        var key = $"{_environment.EnvironmentName}:users";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetUsersQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetUsersQuery());
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

    /// <inheritdoc />
    public async Task<GetUserNotesQueryResult> GetUserNotes(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetUserNotesQuery());

        var userId = _userService.GetLoggedUserId();
        var key = $"{_environment.EnvironmentName}:user:notes:{userId}";
        var value = await _redisDistributedCache.GetObjectAsync<GetUserNotesQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetUserNotesQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetUserNoteQueryResult> GetUserNote(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetUserNoteQuery { UserNoteId = id });

        var key = $"{_environment.EnvironmentName}:user:note:{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetUserNoteQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetUserNoteQuery { UserNoteId = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}