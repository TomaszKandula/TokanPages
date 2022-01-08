namespace TokanPages.WebApi.Services.Caching.Articles;

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using MediatR;

[ExcludeFromCodeCoverage]
public class ArticlesCache : IArticlesCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    public ArticlesCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    public async Task<List<GetAllArticlesQueryResult>> GetArticles(bool isPublished = true, bool noCache = false)
    {
        const string key = "GetAllArticlesQueryResult";
        if (noCache)
            return await _mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });

        var value = await _redisDistributedCache.GetObjectAsync<List<GetAllArticlesQueryResult>>(key);
        if (value is not null && value.Any()) return value;

        value = await _mediator.Send(new GetAllArticlesQuery { IsPublished = isPublished });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    public async Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false)
    {
        var key = $"article-{id:N}";
        if (noCache)
            return await _mediator.Send(new GetArticleQuery { Id = id});

        var value = await _redisDistributedCache.GetObjectAsync<GetArticleQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetArticleQuery { Id = id});
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}