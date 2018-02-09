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
    public class GameDetailsController : ApiController
    {
        public async Task<GameDetail> GetGameDetails(string steamID, string appID, bool getStats = true, string achievementToSearch = null)
        {
            GameDetailsDA da = new GameDetailsDA();
            return await da.GetDataAsync(steamID, appID, getStats, achievementToSearch);
        }

        public async Task<GameDetail> GetGameWithFriendDetails(string steamID, string appID, string friendSteamId, bool getStats = true, string achievementToSearch = null)
        {
            GameDetailsDA da = new GameDetailsDA();
            return await da.GetDataWithFriend(steamID, appID, friendSteamId, getStats, achievementToSearch);
        }
    }
}
