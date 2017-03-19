using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Service.Controllers
{
    public class FriendsController : ApiController
    {
        public async Task<List<Friend>> GetFriends(string steamId)
        {
            FriendsDA da = new FriendsDA();
            return await da.GetDataAsync(steamId);
        }

        public async Task<List<Friend>> GetFriendsWithSameGame(string steamId, string appId)
        {
            FriendsDA da = new FriendsDA();
            return await da.GetFriendsWithSameGame(steamId, appId);
        }
    }
}
