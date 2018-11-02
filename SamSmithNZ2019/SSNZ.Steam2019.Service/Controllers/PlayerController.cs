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
    public class PlayerController : ControllerBase
    {
        private IRedisService _redisService;

        public PlayerController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("{steamID}")]
        public async Task<Player> GetPlayer(string steamID, bool useCache = false)
        {
            PlayerDA da = new PlayerDA();
            return await da.GetDataAsync(_redisService, steamID, useCache);
        }
    }
}
