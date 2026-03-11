namespace ProductService.Services;

public interface ICacheService
{
    Task<T?> GetDataAsync<T>(string key); 

    Task<bool> SetDataAsync<T>(string key, T value, Double expirationTime); 

    // Task<bool> RemoveDataAsync(string key); 
}