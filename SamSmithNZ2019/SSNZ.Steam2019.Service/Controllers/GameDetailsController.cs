using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam2019.Service.DataAccess;
using SSNZ.Steam2019.Service.Models;
using SSNZ.Steam2019.Service.Services;

namespace SSNZ.Steam2019.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameDetailsController : ControllerBase
    {
        private  IRedisService _redisService;

        public GameDetailsController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet]
        public async Task<GameDetail> GetGameDetails(string steamID, string appID, bool getStats = true, string achievementToSearch = null)
        {
            GameDetailsDA da = new GameDetailsDA();
            return await da.GetDataAsync(_redisService, steamID, appID, getStats, achievementToSearch);
        }

        //// GET
        //[HttpGet]
        //public async Task<GameDetail> GetGameWithFriendDetails(string steamID, string appID, string friendSteamId, bool getStats = true, string achievementToSearch = null)
        //{
        //    GameDetailsDA da = new GameDetailsDA();
        //    return await da.GetDataWithFriend(steamID, appID, friendSteamId, getStats, achievementToSearch);
        //}
    }
}
