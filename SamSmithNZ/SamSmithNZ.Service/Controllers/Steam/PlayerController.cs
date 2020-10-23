﻿using System;
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
    public class PlayerController : ControllerBase
    {
        private IRedisService _redisService;

        public PlayerController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetPlayer")]
        public async Task<Player> GetPlayer(string steamID, bool useCache = true)
        {
            PlayerDA da = new PlayerDA();
            return await da.GetDataAsync(_redisService, steamID, useCache);
        }
    }
}
