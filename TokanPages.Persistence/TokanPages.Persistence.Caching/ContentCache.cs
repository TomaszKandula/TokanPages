using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Hosting;
using TokanPages.Backend.Application.Content.Components.Commands;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Persistence.Caching;

/// <summary>
/// Content cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
internal sealed class ContentCache : IContentCache
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

        var key = $"{_environment.EnvironmentName}:content:component:manifest";
        var value = await _redisDistributedCache.GetObjectAsync<GetContentManifestQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetContentManifestQuery());
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<GetContentQueryResult> GetContent(string? language,  string name = "", bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetContentQuery { ContentName = name, Language = language });

        var key = $"{_environment.EnvironmentName}:content:{name}:{language}";
        var value = await _redisDistributedCache.GetObjectAsync<GetContentQueryResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(new GetContentQuery { ContentName = name, Language = language });
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    /// <inheritdoc />
    public async Task<RequestPageDataCommandResult> GetPageContent(RequestPageDataCommand request, bool noCache = false)
    {
        var command = new RequestPageDataCommand
        {
            Components = request.Components,
            PageName = request.PageName,
            Language = request.Language
        };

        if (noCache)
            return await _mediator.Send(command);

        var componentKey = GetUniqueKey(request.Components);
        var key = $"{_environment.EnvironmentName}:content:{componentKey}:{request.Language}";
        var value = await _redisDistributedCache.GetObjectAsync<RequestPageDataCommandResult>(key);
        if (value is not null) return value;

        value = await _mediator.Send(command);
        await _redisDistributedCache.SetObjectAsync(key, value);

        return value;
    }

    private static string GetUniqueKey(IEnumerable<ContentModel> components)
    {
        var data = components.Aggregate(string.Empty, (current, item) => $"{current}{item.ContentName}");
        return data.ToBase64Encode();
    }
}