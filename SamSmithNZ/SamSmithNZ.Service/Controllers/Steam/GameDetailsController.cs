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
    public class GameDetailsController : ControllerBase
    {
        private IRedisService _redisService;

        public GameDetailsController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet("GetGameDetails")]
        public async Task<GameDetail> GetGameDetails(string steamID, string appID, bool getStats = true, string achievementToSearch = null, bool useCache = true)
        {
            if (string.IsNullOrEmpty(steamID) == false && string.IsNullOrEmpty(appID) == false)
            {
                GameDetailsDA da = new GameDetailsDA();
                return await da.GetDataAsync(_redisService, steamID, appID, getStats, achievementToSearch, useCache);
            }
            else
            {
                return null;
            }
        }

        //// GET
        //[HttpGet("GetGameWithFriendDetails")]
        //public async Task<GameDetail> GetGameWithFriendDetails(string steamID, string appID, string friendSteamId, bool getStats = true, string achievementToSearch = null, bool useCache = true)
        //{
        //    GameDetailsDA da = new GameDetailsDA();
        //    return await da.GetDataWithFriendAsync(_redisService, steamID, appID, friendSteamId, getStats, achievementToSearch, useCache);
        //}
    }
}
