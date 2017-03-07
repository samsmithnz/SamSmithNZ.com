using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class GlobalAchievementsDA
    {
        public Game GetData(string steamID, string appID)
        {

            SteamGlobalAchievementPercentagesForAppDA da = new SteamGlobalAchievementPercentagesForAppDA();
            SteamGlobalAchievementsForApp globalData = da.GetData(appID);
            return null;
        }
    }
}
