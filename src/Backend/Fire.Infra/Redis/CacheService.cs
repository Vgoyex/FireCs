using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace Fire.Infra.Redis
{
    public class CacheService //: ICacheService
    {
        //private readonly IDatabase _db;

        //public CacheService(IConnectionMultiplexer redis)
        //{
        //    _db = redis.GetDatabase();
        //}

        //public Task SetAsync(string key, string value, TimeSpan ttl)
        //    => _db.StringSetAsync(key, value, ttl);

        //public Task<string?> GetAsync(string key)
        //    => _db.StringGetAsync(key);
    }
}
