using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

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

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IUserService _userService;

    /// <summary>
    /// Articles cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    /// <param name="jsonSerializer">JSON Serializer instance</param>
    /// <param name="userService">USer service instance</param>
    public ArticlesCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment, 
        IJsonSerializer jsonSerializer, IUserService userService)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
        _jsonSerializer = jsonSerializer;
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<GetArticlesQueryResult> GetArticles(GetArticlesQuery query, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(query);

        var userLanguage = _userService.GetRequestUserLanguage();
        var uniqueKey = $"{query.IsPublished}:{query.CategoryId}:{query.SearchTerm}:{query.PageNumber}:{query.PageSize}:{query.OrderByAscending}:{query.OrderByColumn}:{userLanguage}";
        var key = $"{_environment.EnvironmentName}:articles:{uniqueKey}";
        var value = await _redisDistributedCache.GetObjectAsync<GetArticlesQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(query);
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetArticleQuery { Id = id });

        var userLanguage = _userService.GetRequestUserLanguage();
        var key = $"{_environment.EnvironmentName}:article:{id}:{userLanguage}";
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

        var userLanguage = _userService.GetRequestUserLanguage();
        var key = $"{_environment.EnvironmentName}:article:{title}:{userLanguage}";
        var value = await _redisDistributedCache.GetObjectAsync<GetArticleQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetArticleQuery { Title = title });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<RetrieveArticleInfoCommandResult> RetrieveArticleInfo(List<Guid> articleIds, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new RetrieveArticleInfoCommand { ArticleIds = articleIds });

        var list = _jsonSerializer.Serialize(articleIds);
        var userLanguage = _userService.GetRequestUserLanguage();
        var key = $"{_environment.EnvironmentName}:article:info:ids:{list}:{userLanguage}";
        var value = await _redisDistributedCache.GetObjectAsync<RetrieveArticleInfoCommandResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new RetrieveArticleInfoCommand { ArticleIds = articleIds });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}