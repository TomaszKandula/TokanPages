using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Currencies.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Currencies cache implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CurrenciesCache : ICurrenciesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Currencies cache implementation.
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="mediator">Mediator instance.</param>
    public CurrenciesCache(IRedisDistributedCache redisDistributedCache, IHostEnvironment environment, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _environment = environment;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<IList<GetCurrencyCodesQueryResult>> GetCurrencyCodes(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetCurrencyCodesQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:currency:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetCurrencyCodesQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetCurrencyCodesQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}