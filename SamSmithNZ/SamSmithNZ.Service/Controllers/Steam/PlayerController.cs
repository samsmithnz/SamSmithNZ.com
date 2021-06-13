using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.Steam;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.Steam
{
    [Route("api/Steam/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public PlayerController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetPlayer")]
        public async Task<Player> GetPlayer(string steamID, bool useCache = true)
        {
            if (string.IsNullOrEmpty(steamID) == false)
            {
                PlayerDA da = new();
                return await da.GetDataAsync(_redisService, steamID, useCache);
            }
            else
            {
                return null;
            }
        }
    }
}
