using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class PlayerAchievementsDA
    {
        public SteamPlayerAchievementsForApp GetData(string steamID, string appID)
        {

            SteamPlayerAchievementsForAppDA da = new SteamPlayerAchievementsForAppDA();
            SteamPlayerAchievementsForApp playerData = da.GetData(steamID, appID);
            return playerData;
        }
    }
}
