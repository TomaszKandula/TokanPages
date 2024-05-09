using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Articles cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticlesCache : IArticlesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Articles cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    public ArticlesCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<List<GetArticlesQueryResult>> GetArticles(bool isPublished = true, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetArticlesQuery { IsPublished = isPublished });

        var key = $"{_environment.EnvironmentName}:articles";
        var value = await _redisDistributedCache.GetObjectAsync<List<GetArticlesQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetArticlesQuery { IsPublished = isPublished });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetArticleQuery { Id = id });

        var key = $"{_environment.EnvironmentName}:article:{id}";
        var value = await _redisDistributedCache.GetObjectAsync<GetArticleQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetArticleQuery { Id = id });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetArticleQueryResult> GetArticle(string title, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetArticleQuery { Title = title });

        var key = $"{_environment.EnvironmentName}:article:{title}";
        var value = await _redisDistributedCache.GetObjectAsync<GetArticleQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetArticleQuery { Title = title });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}