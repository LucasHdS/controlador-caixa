using Application.Abstractions.Cache;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Cache;
public class RedisCacheService<T>(IDistributedCache cache) : ICacheService<T>
{
    public async Task<T?> GetAsync(string key)
    {
        var data = await cache.GetStringAsync(key);
        return data is null ? default : JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, json);
    }
}
