namespace TokanPages.WebApi.Services.Caching;

using System.Threading.Tasks;

/// <summary>
/// Redis distributed cache contract
/// </summary>
public interface IRedisDistributedCache
{
    /// <summary>
    /// Calls the data we want to cache.
    /// </summary>
    /// <typeparam name="TEntity">Data type.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <returns>Cached data.</returns>
    TEntity GetObject<TEntity>(string key);

    /// <summary>
    /// Calls the data we want to cache.
    /// </summary>
    /// <typeparam name="TEntity">Data type.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <returns>Cached data.</returns>
    Task<TEntity> GetObjectAsync<TEntity>(string key);

    /// <summary>
    /// Caches any type of data we want.
    /// </summary>
    /// <typeparam name="TEntity">Data type.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Value to be set.</param>
    /// <param name="absoluteExpirationMinute">Duration.</param>
    /// <param name="slidingExpirationSecond">Elongation time.</param>
    void SetObject<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, int slidingExpirationSecond = 0);

    /// <summary>
    /// Caches any type of data we want.
    /// </summary>
    /// <typeparam name="TEntity">Data type.</typeparam>
    /// <param name="key">Cache key.</param>
    /// <param name="value">Value to be set.</param>
    /// <param name="absoluteExpirationMinute">Duration.</param>
    /// <param name="slidingExpirationSecond">Elongation time.</param>
    Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, int slidingExpirationSecond = 0);

    /// <summary>
    /// Removes from cache.
    /// </summary>
    /// <param name="key">Key.</param>
    void Remove(string key);

    /// <summary>
    /// Removes from cache.
    /// </summary>
    /// <param name="key">Key.</param>
    Task RemoveAsync(string key);
}