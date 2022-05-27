namespace TokanPages.WebApi.Services.Caching.Assets;

using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Backend.Cqrs.Handlers.Queries.Assets;
using TokanPages.Services.HttpClientService.Models;
using MediatR;

/// <summary>
/// Assets cache implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class AssetsCache : IAssetsCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly IMediator _mediator;

    /// <summary>
    /// Asset cache implementation
    /// </summary>
    /// <param name="redisDistributedCache">Redis distributed cache instance</param>
    /// <param name="mediator">Mediator instance</param>
    public AssetsCache(IRedisDistributedCache redisDistributedCache, IMediator mediator)
    {
        _redisDistributedCache = redisDistributedCache;
        _mediator = mediator;
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetAsset(string blobName, bool noCache = false)
    {
        if (noCache)
            return await _mediator.Send(new GetSingleAssetQuery { BlobName = blobName });

        var cache = await _redisDistributedCache.GetObjectAsync<HttpContentResult>(blobName);
        if (cache is not null)
            return new FileContentResult(cache.Content!, cache.ContentType?.MediaType!);

        var result = await _mediator.Send(new GetSingleAssetQuery { BlobName = blobName });
        await SaveToCache(result, blobName);

        return result;
    }

    /// <inheritdoc />
    public async Task<IActionResult> GetArticleAsset(string id, string assetName, bool noCache = false)
    {
        var key = $"{id}/{assetName}";

        if (noCache)
            return await _mediator.Send(new GetArticleAssetQuery {  Id = id, AssetName = assetName });

        var cache = await _redisDistributedCache.GetObjectAsync<HttpContentResult>(key);
        if (cache is not null)
            return new FileContentResult(cache.Content!, cache.ContentType?.MediaType!);

        var result = await _mediator.Send(new GetArticleAssetQuery {  Id = id, AssetName = assetName });
        await SaveToCache(result, key);

        return result;
    }

    private async Task SaveToCache(FileContentResult content, string key)
    {
        // We will not cache asset which is larger than 1.44 MB
        if (content.FileContents.Length > 1.44 * 1024 * 1024)
            return;

        var value = new HttpContentResult
        {
            Content = content.FileContents,
            ContentType = new MediaTypeHeaderValue(content.ContentType)
        };

        await _redisDistributedCache.SetObjectAsync(key, value);
    }
}