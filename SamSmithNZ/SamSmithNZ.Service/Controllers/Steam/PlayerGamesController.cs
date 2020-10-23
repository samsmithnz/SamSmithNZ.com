using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.Steam;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Service.Controllers.Steam
{
    [Route("api/Steam/[controller]")]
    [ApiController]
    public class PlayerGamesController : ControllerBase
    {
        private IRedisService _redisService;

        public PlayerGamesController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetPlayerGames")]
        public async Task<List<Game>> GetPlayerGames(string steamID, bool useCache = true)
        {
            PlayerGamesDA da = new PlayerGamesDA();
            return await da.GetDataAsync(_redisService, steamID, useCache);
        }
    }
}
