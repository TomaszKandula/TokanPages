using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.RedisCacheService.Abstractions;

namespace TokanPages.Services.RedisCacheService;

/// <summary>
/// Redis distributed implementation
/// </summary>
[ExcludeFromCodeCoverage]
public class RedisDistributedCache : IRedisDistributedCache
{
    private readonly IDistributedCache _distributedCache;

    private readonly AppSettings _appSettings;

    /// <summary>
    /// Redis distributed implementation
    /// </summary>
    /// <param name="distributedCache">DistributedCache instance</param>
    /// <param name="configuration">Configuration instance</param>
    public RedisDistributedCache(IDistributedCache distributedCache, IOptions<AppSettings> configuration)
    {
        _distributedCache = distributedCache;
        _appSettings = configuration.Value;
    }

    /// <inheritdoc />
    public TEntity? GetObject<TEntity>(string key)
    {
        VerifyArguments(new[] { key });

        var cachedValue = _distributedCache.GetString(key);
        return string.IsNullOrEmpty(cachedValue) ? default : JsonConvert.DeserializeObject<TEntity>(cachedValue);
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetObjectAsync<TEntity>(string key, CancellationToken cancellationToken = default)
    {
        VerifyArguments(new[] { key });

        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        return string.IsNullOrEmpty(cachedValue) ? default : JsonConvert.DeserializeObject<TEntity>(cachedValue);
    }

    /// <inheritdoc />
    public void SetObject<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, int slidingExpirationSecond = 0)
    {
        VerifyArguments(new[] { key });
        var serializedObject = JsonConvert.SerializeObject(value);
        _distributedCache.SetString(key, serializedObject, 
            SetDistributedCacheEntryOptions(absoluteExpirationMinute, slidingExpirationSecond));            
    }

    /// <inheritdoc />
    public async Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, 
        int slidingExpirationSecond = 0, CancellationToken cancellationToken = default)
    {
        VerifyArguments(new[] { key });
        var serializedObject = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(key, serializedObject, 
            SetDistributedCacheEntryOptions(absoluteExpirationMinute, slidingExpirationSecond), cancellationToken);
    }

    /// <inheritdoc />
    public void Remove(string key)
    {
        VerifyArguments(new[] { key });
        _distributedCache.Remove(key);            
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        VerifyArguments(new[] { key });
        await _distributedCache.RemoveAsync(key, cancellationToken);            
    }

    private static void VerifyArguments(IEnumerable<string> arguments)
    {
        foreach (var argument in arguments)
        {
            if (!string.IsNullOrWhiteSpace(argument)) continue;
            const string message = $"The argument '{nameof(argument)}' cannot be null or empty";
            throw new BusinessException(ErrorCodes.ARGUMENT_EMPTY_OR_NULL, message);
        }
    }

    private DistributedCacheEntryOptions SetDistributedCacheEntryOptions(int absoluteExpirationMinute, int slidingExpirationSecond)
    {
        var expirationMinute = absoluteExpirationMinute == 0
            ? _appSettings.AzRedisExpirationMinute
            : absoluteExpirationMinute;

        var expirationSecond = slidingExpirationSecond == 0
            ? _appSettings.AzRedisExpirationSecond
            : slidingExpirationSecond;

        return new DistributedCacheEntryOptions
        {
            AbsoluteExpiration = DateTime.Now.AddMinutes(expirationMinute),
            SlidingExpiration = TimeSpan.FromSeconds(expirationSecond)
        };
    }
}