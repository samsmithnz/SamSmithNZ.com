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
    public class FriendsController : ControllerBase
    {
        private IRedisService _redisService;

        public FriendsController(IRedisService redisService)
        {
            _redisService = redisService;
        }

        // GET
        [HttpGet]
        public async Task<List<Friend>> GetFriends(string steamId)
        {
            FriendsDA da = new FriendsDA();
            return await da.GetDataAsync(_redisService, steamId);
        }

        // GET
        [HttpGet]
        public async Task<List<Friend>> GetFriendsWithSameGame(string steamId, string appId)
        {
            FriendsDA da = new FriendsDA();
            return await da.GetFriendsWithSameGame(_redisService, steamId, appId);
        }
    }
}
