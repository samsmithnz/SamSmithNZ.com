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
    public class PlayerAchievementsController : ApiController
    {
        public SteamPlayerAchievementsForApp GetPlayerAchievements(string steamID, string appID)
        {
            PlayerAchievementsDA da = new PlayerAchievementsDA();
            return da.GetData(steamID, appID);
        }
    }
}
