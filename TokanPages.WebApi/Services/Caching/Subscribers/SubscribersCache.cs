namespace TokanPages.WebApi.Services.Caching.Subscribers;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Backend.Cqrs.Handlers.Queries.Subscribers;
using MediatR;

[ExcludeFromCodeCoverage]
public class SubscribersCache : ISubscribersCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    public SubscribersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public async Task<IEnumerable<GetAllSubscribersQueryResult>> GetSubscribers(bool noCache = false)
    {
        const string key = "GetAllSubscribersQueryResult";
        if (noCache)
            return await _mediator.Send(new GetAllSubscribersQuery());

        var value = await _redisDistributedCache.GetObjectAsync<IEnumerable<GetAllSubscribersQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllSubscribersQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    public async Task<GetSubscriberQueryResult> GetSubscriber(Guid id, bool noCache = false)
    {
        var key = $"subscriber-{id:N}";
        if (noCache)
            return await _mediator.Send(new GetSubscriberQuery { Id = id });

        var value = await _redisDistributedCache.GetObjectAsync<GetSubscriberQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetSubscriberQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}