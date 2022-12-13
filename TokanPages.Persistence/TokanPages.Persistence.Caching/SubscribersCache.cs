using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Subscribers.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Subscribers cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class SubscribersCache : ISubscribersCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Subscribers cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    public SubscribersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<List<GetAllSubscribersQueryResult>> GetSubscribers(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetAllSubscribersQuery());

        var key = $"{_environment.EnvironmentName}:subscribers/";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetAllSubscribersQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllSubscribersQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetSubscriberQueryResult> GetSubscriber(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetSubscriberQuery { Id = id });

        var key = $"{_environment.EnvironmentName}:subscriber/{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetSubscriberQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetSubscriberQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}