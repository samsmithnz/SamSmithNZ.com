using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSNZ.Steam.CoreData;
using SSNZ.Steam.CoreModels;

namespace SSNZ.Steam.CoreService.Controllers
{
    public class GameDetailsController : Controller
    {
        public async Task<GameDetail> GetGameDetails(string steamID, string appID)
        {
            GameDetailsDA da = new GameDetailsDA();
            return await da.GetDataAsync(steamID, appID);            
        }

        public async Task<GameDetail> GetGameWithFriendDetails(string steamID, string appID, string friendSteamId)
        {
            GameDetailsDA da = new GameDetailsDA();
            return await da.GetDataWithFriend(steamID, appID, friendSteamId);
        }
    }
}
