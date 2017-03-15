using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSNZ.Steam.Data;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Service.Controllers
{
    public class GameDetailsController : ApiController
    {
        public GameDetail GetGameDetails(string steamID, string appID)
        {
            GameDetailsDA da = new GameDetailsDA();
            return da.GetData(steamID, appID);
        }

        public GameDetail GetGameWithFriendDetails(string steamID, string appID, string friendSteamId)
        {
            GameDetailsDA da = new GameDetailsDA();
            return da.GetDataWithFriend(steamID, appID, friendSteamId);
        }
    }
}
