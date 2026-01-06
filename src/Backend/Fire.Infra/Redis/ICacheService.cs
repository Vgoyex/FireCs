namespace Fire.Infra.Redis
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan ttl);
        Task<string?> GetAsync(string key);
    }
}
