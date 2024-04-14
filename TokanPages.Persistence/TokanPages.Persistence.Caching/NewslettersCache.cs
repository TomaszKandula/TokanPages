using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Newsletters.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Subscribers cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class NewslettersCache : INewslettersCache
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
    public NewslettersCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<List<GetAllNewslettersQueryResult>> GetNewsletters(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetAllNewslettersQuery());

        var key = $"{_environment.EnvironmentName}:newsletters";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetAllNewslettersQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllNewslettersQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetNewsletterQueryResult> GetNewsletter(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetNewsletterQuery { Id = id });

        var key = $"{_environment.EnvironmentName}:newsletter:{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetNewsletterQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetNewsletterQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}