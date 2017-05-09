using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam.CoreData;
using SSNZ.Steam.CoreModels;

namespace SSNZ.Steam.CoreService.Controllers
{
    public class FriendsController : Controller
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
