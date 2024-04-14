using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Payments.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Payments cache implementation.
/// </summary>
public class PaymentsCache : IPaymentsCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Payments cache implementation.
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance.</param>
    /// <param name="environment">Host environment instance.</param>
    /// <param name="mediator">Mediator instance.</param>
    public PaymentsCache(IRedisDistributedCache redisDistributedCache, IHostEnvironment environment, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _environment = environment;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<IList<GetPaymentStatusListQueryResult>> GetPaymentStatusList(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetPaymentStatusListQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:payment:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetPaymentStatusListQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetPaymentStatusListQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<IList<GetPaymentTypeListQueryResult>> GetPaymentTypeList(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetPaymentTypeListQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:payment:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetPaymentTypeListQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetPaymentTypeListQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}