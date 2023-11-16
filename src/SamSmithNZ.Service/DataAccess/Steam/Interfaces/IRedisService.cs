using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam.Interfaces
{
    public interface IRedisService
    {
        Task<string> GetAsync(string key);
        //Task<bool> ExistsAsync(string key);
        Task<bool> SetAsync(string key, string data, TimeSpan expirationTime);
    }
}
