using System.Threading.Tasks;
using StackExchange.Redis;
using System;
using System.Diagnostics;

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
            string result = await _database.StringGetAsync(key);
            if (string.IsNullOrEmpty(result)==false)
            {
                Debug.WriteLine("Getting item from cache: " + key);
            }
            return result;
        }

        //public async Task<bool> ExistsAsync(string key)
        //{
        //    return await _database.KeyExistsAsync(key);
        //}

        public async Task<bool> SetAsync(string key, string data, TimeSpan expirationTime)
        {
            Debug.WriteLine("Setting item into cache: " + key);
            return await _database.StringSetAsync(key, data, expirationTime);
        }
    }
}
