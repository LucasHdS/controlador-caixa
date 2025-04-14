namespace Application.Abstractions.Cache;
public interface ICacheService<T>
{
    Task<T?> GetAsync(string key);
    Task SetAsync(string key, T value);
}