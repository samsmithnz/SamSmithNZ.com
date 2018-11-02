using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam2019.Service.DataAccess;
using SSNZ.Steam2019.Service.Models;
using SSNZ.Steam2019.Service.Services;

namespace SSNZ.Steam2019.Service.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerGamesController : ControllerBase
    {
        private IRedisService _redisService;

        public PlayerGamesController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetPlayer")]
        public async Task<List<Game>> GetPlayer(string steamID, bool useCache = true)
        {
            PlayerGamesDA da = new PlayerGamesDA();
            return await da.GetDataAsync(_redisService, steamID, useCache);
        }
    }
}
