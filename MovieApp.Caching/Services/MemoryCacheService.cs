using Microsoft.Extensions.Caching.Memory;
using MovieApp.Caching.Configurations;
using MovieApp.Caching.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Caching.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheSettings;

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> factory)
        {
            if (_memoryCache.TryGetValue(cacheKey, out T cachedItem))
            {
                return cachedItem; // Return the cached item if found
            }

            // Create item and set it in the cache
            var newItem = await factory();
            if (newItem != null)
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    // Set expiration time by retreived value dynamically from appsettings
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_cacheSettings.AbsoluteExpirationInMinutes)
                };
                _memoryCache.Set(cacheKey, newItem, cacheEntryOptions);
            }
            return newItem;
        }

        public Task SetAsync<T>(string cacheKey, T item, TimeSpan? absoluteExpiration = null)
        {
            _memoryCache.Set(cacheKey, item, absoluteExpiration.HasValue ? new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpiration.Value
            } : null);
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
            return Task.CompletedTask;
        }
    }
}
