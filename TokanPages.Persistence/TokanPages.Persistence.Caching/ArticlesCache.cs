using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using MediatR;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Articles cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticlesCache : IArticlesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    /// <summary>
    /// Articles cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="mediator">Mediator instance</param>
    public ArticlesCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<List<GetAllArticlesQueryResult>> GetArticles(bool isPublished = true, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });

        const string key = "articles/";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetAllArticlesQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetArticleQuery { Id = id});

        var key = $"article/{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetArticleQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetArticleQuery { Id = id});
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}