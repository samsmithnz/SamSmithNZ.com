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
    public class GameDetailsOldController : ApiController
    {
        public GameDetail GetGameDetailsOld(string steamID, string appID)
        {
            GameDetailsOldDA da = new GameDetailsOldDA();
            return da.GetDataOld(steamID, appID);
        }

        public GameDetail GetGameWithFriendDetailsOld(string steamID, string appID, string friendSteamId)
        {
            GameDetailsOldDA da = new GameDetailsOldDA();
            return da.GetDataWithFriendOld(steamID, appID, friendSteamId);
        }
    }
}
