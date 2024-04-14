using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Invoicing.Countries.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Countries cache implementation.
/// </summary>
[ExcludeFromCodeCoverage]
public class CountriesCache : ICountriesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Countries cache implementation.
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="mediator">Mediator instance.</param>
    public CountriesCache(IRedisDistributedCache redisDistributedCache, IHostEnvironment environment, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _environment = environment;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<IList<GetCountryCodesQueryResult>> GetCountryCodes(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetCountryCodesQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:country:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetCountryCodesQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetCountryCodesQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}