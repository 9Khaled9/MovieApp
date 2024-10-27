using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Caching.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetOrCreateAsync<T>(string cacheKey, Func<Task<T>> createItem);
        Task SetAsync<T>(string cacheKey, T item, TimeSpan? absoluteExpiration = null);
        Task RemoveAsync(string cacheKey);
    }
}
