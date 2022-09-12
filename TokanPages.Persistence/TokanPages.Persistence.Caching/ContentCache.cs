using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Handlers.Queries.Content;
using MediatR;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Content cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class ContentCache : IContentCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    /// <summary>
    /// Content cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="mediator">Mediator instance</param>
    public ContentCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<GetContentQueryResult> GetContent(string? language, string type = "", string name = "", bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });

        var key = $"content/{type}/{name}/{language}";
        var value = await _redisDistributedCache.GetObjectAsync<GetContentQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}