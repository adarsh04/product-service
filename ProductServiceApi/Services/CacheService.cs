using Microsoft.Extensions.Caching.Distributed;
using System.Reflection.Metadata;
using System.Text.Json;

namespace ProductService.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetDataAsync<T>(string key)
    {
        var cachedData = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<T>(cachedData);
        }
        return default;
    }

    public async Task<bool> SetDataAsync<T>(string key, T value, Double expirationTime)
    {
         var options = new DistributedCacheEntryOptions()
        .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationTime));         
        await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), options);
        return true;
    }
}    