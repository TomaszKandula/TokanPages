namespace TokanPages.WebApi.Services.Caching
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Backend.Shared.Services;
    using Newtonsoft.Json;

    public class RedisDistributedCache : IRedisDistributedCache
    {
        private readonly IDistributedCache _distributedCache;

        private readonly IApplicationSettings _applicationSettings;
        
        public RedisDistributedCache(IDistributedCache distributedCache, IApplicationSettings applicationSettings)
        {
            _distributedCache = distributedCache;
            _applicationSettings = applicationSettings;
        }

        public TEntity GetObject<TEntity>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var cachedValue = _distributedCache.GetString(key);
            return string.IsNullOrEmpty(cachedValue) 
                ? default 
                : JsonConvert.DeserializeObject<TEntity>(cachedValue);
        }

        public async Task<TEntity> GetObjectAsync<TEntity>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var cachedValue = await _distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(cachedValue) 
                ? default 
                : JsonConvert.DeserializeObject<TEntity>(cachedValue);
        }

        public void SetObject<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, int slidingExpirationSecond = 0)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var serializedObject = JsonConvert.SerializeObject(value);
            _distributedCache.SetString(key, serializedObject, SetDistributedCacheEntryOptions(absoluteExpirationMinute, slidingExpirationSecond));            
        }

        public async Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 0, int slidingExpirationSecond = 0)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var serializedObject = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, serializedObject, SetDistributedCacheEntryOptions(absoluteExpirationMinute, slidingExpirationSecond));
        }

        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _distributedCache.Remove(key);            
        }

        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            await _distributedCache.RemoveAsync(key);            
        }

        private DistributedCacheEntryOptions SetDistributedCacheEntryOptions(int absoluteExpirationMinute, int slidingExpirationSecond)
        {
            var expirationMinute = absoluteExpirationMinute == 0
                ? _applicationSettings.AzureRedis.ExpirationMinute
                : absoluteExpirationMinute;

            var expirationSecond = slidingExpirationSecond == 0
                ? _applicationSettings.AzureRedis.ExpirationSecond
                : slidingExpirationSecond;
            
            return new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(expirationMinute),
                SlidingExpiration = TimeSpan.FromSeconds(expirationSecond)
            };
        }
    }
}