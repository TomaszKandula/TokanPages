using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Content.Queries;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Content cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class ContentCache : IContentCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IHostEnvironment _environment;

    private readonly IMediator _mediator;

    /// <summary>
    /// Content cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="environment">Host environment instance</param>
    /// <param name="mediator">Mediator instance</param>
    public ContentCache(IRedisDistributedCache redisDistributedCache, IMediator mediator, IHostEnvironment environment)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<GetContentManifestQueryResult> GetContentManifest(bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetContentManifestQuery());

        var key = $"{_environment.EnvironmentName}:content/component/manifest";
        var value = await _redisDistributedCache.GetObjectAsync<GetContentManifestQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetContentManifestQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetContentQueryResult> GetContent(string? language, string type = "", string name = "", bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });

        var key = $"{_environment.EnvironmentName}:content/{type}/{name}/{language}";
        var value = await _redisDistributedCache.GetObjectAsync<GetContentQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetContentQuery { Type = type, Name = name, Language = language });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }
}