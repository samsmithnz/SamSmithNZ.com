using System.Threading.Tasks;
using StackExchange.Redis;
using System;

namespace SSNZ.Steam2019.Service.Services
{

    public class RedisService : IRedisService
    {
        private IDatabase _database;

        public RedisService(IDatabase database)
        {
            _database = database;
        }

        public async Task<string> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task<bool> SetAsync(string key, string data, TimeSpan expirationTime)
        {
            return await _database.StringSetAsync(key, data, expirationTime);
        }
    }
}
