using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.Steam;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.Steam
{
    [Route("api/Steam/[controller]")]
    [ApiController]
    public class PlayerGamesController : ControllerBase
    {
        private readonly IRedisService _redisService;

        public PlayerGamesController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetPlayerGames")]
        public async Task<List<Game>> GetPlayerGames(string steamID, bool useCache = true)
        {
            if (string.IsNullOrEmpty(steamID) == false)
            {
                PlayerGamesDA da = new();
                return await da.GetDataAsync(_redisService, steamID, useCache);
            }
            else
            {
                return null;
            }
        }
    }
}
