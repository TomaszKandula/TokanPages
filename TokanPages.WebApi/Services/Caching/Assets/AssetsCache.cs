namespace TokanPages.WebApi.Services.Caching.Assets;

using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Backend.Shared.Services;
using Backend.Shared.Resources;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Models;

[ExcludeFromCodeCoverage]
public class AssetsCache : IAssetsCache
{
    private readonly IRedisDistributedCache _redisDistributedCache;

    private readonly ICustomHttpClient _customHttpClient;
        
    private readonly IApplicationSettings _applicationSettings;
        
    public AssetsCache(IRedisDistributedCache redisDistributedCache, ICustomHttpClient customHttpClient, 
        IApplicationSettings applicationSettings)
    {
        _redisDistributedCache = redisDistributedCache;
        _customHttpClient = customHttpClient;
        _applicationSettings = applicationSettings;
    }

    public async Task<IActionResult> GetAsset(string blobName, bool noCache = false)
    {
        var requestUrl = $"{_applicationSettings.AzureStorage.BaseUrl}/content/assets/{blobName}";
            
        if (noCache)
            return await ExecuteRequest(requestUrl);

        var value = await _redisDistributedCache.GetObjectAsync<HttpContentResult>(blobName);
        if (value is not null)
            return new FileContentResult(value.Content!, value.ContentType?.MediaType!);

        var request = await ExecuteRequest(requestUrl);
        value = new HttpContentResult();

        if (request is not FileContentResult result)
            return new FileContentResult(value.Content!, value.ContentType?.MediaType!);

        value.Content = result.FileContents;
        value.ContentType = new MediaTypeHeaderValue(result.ContentType);

        await _redisDistributedCache.SetObjectAsync(blobName, value);
        return new FileContentResult(value.Content, value.ContentType?.MediaType!);
    }

    public async Task<IActionResult> GetArticleAsset(string id, string assetName, bool noCache = false)
    {
        var key = $"{id}/{assetName}";
        var requestUrl = $"{_applicationSettings.AzureStorage.BaseUrl}/content/articles/{id}/{assetName}";

        if (noCache)
            return await ExecuteRequest(requestUrl);

        var value = await _redisDistributedCache.GetObjectAsync<HttpContentResult>(key);
        if (value is not null)
            return new FileContentResult(value.Content!, value.ContentType?.MediaType!);

        var request = await ExecuteRequest(requestUrl);
        value = new HttpContentResult();

        if (request is not FileContentResult result)
            return new FileContentResult(value.Content!, value.ContentType?.MediaType!);

        value.Content = result.FileContents;
        value.ContentType = new MediaTypeHeaderValue(result.ContentType);

        await _redisDistributedCache.SetObjectAsync(key, value);
        return new FileContentResult(value.Content, value.ContentType?.MediaType!);
    }

    private async Task<IActionResult> ExecuteRequest(string requestUrl)
    {
        var configuration = new Configuration { Url = requestUrl, Method = "GET" };
        var results = await _customHttpClient.Execute(configuration);
        if (results.StatusCode == HttpStatusCode.OK)
            return new FileContentResult(results.Content!, results.ContentType?.MediaType!);

        return new ContentResult
        {
            StatusCode = (int)results.StatusCode,
            ContentType = results.ContentType?.MediaType,
            Content = results.Content is null 
                ? ErrorCodes.ERROR_UNEXPECTED 
                : Encoding.Default.GetString(results.Content)
        };
    }
}