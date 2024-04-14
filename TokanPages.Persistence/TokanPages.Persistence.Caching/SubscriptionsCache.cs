using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Subscriptions.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Subscription cache implementation.
/// </summary>
public class SubscriptionsCache : ISubscriptionsCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Subscription cache implementation.
    /// </summary>
    /// <param name="redisDistributedCache">REDIS Distributed cache instance.</param>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="environment">Host environment instance.</param>
    public SubscriptionsCache(IRedisDistributedCache redisDistributedCache, IHostEnvironment environment, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _environment = environment;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<GetSubscriptionPricesQueryResult> GetSubscriptionPrices(string languageIso, bool noCache = false)
    {
        var query = new GetSubscriptionPricesQuery { LanguageIso = languageIso};
        if (noCache)
            return await _mediator.Send(query);

        var key = $"{_environment.EnvironmentName}:subscription:{languageIso}:prices";
        var value = await _redisDistributedCache.GetObjectAsync<GetSubscriptionPricesQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(query);
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}