using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Invoicing.Payments.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Payments cache implementation.
/// </summary>
[ExcludeFromCodeCoverage]
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
    public async Task<IList<GetPaymentStatusQueryResult>> GetPaymentStatus(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetPaymentStatusQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:payment:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetPaymentStatusQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetPaymentStatusQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<IList<GetPaymentTypeQueryResult>> GetPaymentType(string filterBy, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetPaymentTypeQuery { FilterBy = filterBy });

        var key = $"{_environment.EnvironmentName}:payment:codes";
        var value = await _redisDistributedCache.GetObjectAsync<IList<GetPaymentTypeQueryResult>>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetPaymentTypeQuery { FilterBy = filterBy });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}