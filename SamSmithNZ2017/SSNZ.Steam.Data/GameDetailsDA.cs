using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class GameDetailsDA
    {
        public GameDetail GetData(string steamID, string appID)
        {
            SteamGameDetailDA da = new SteamGameDetailDA();
            SteamGameDetail gameDetail = da.GetData(appID);

            GameAchievementsDA da2 = new GameAchievementsDA();

            GameDetail results = new GameDetail();
            results.AppID = appID;
            results.GameName = gameDetail.game.gameName;
            results.Achievements = da2.GetData(steamID, appID, gameDetail);

            //Calculate the total achieved items
            int totalArchieved = 0;
            foreach (Achievement item in results.Achievements)
            {
                if (item.Achieved == 1)
                {
                    totalArchieved++;
                }
            }
            results.TotalArchieved = totalArchieved;
            if (results.Achievements.Count > 0)
            {
                results.PercentAchieved = (decimal)totalArchieved / (decimal)results.Achievements.Count;
            }

            return results;
        }

        private decimal GetGlobalAchievement(string apiName, List<SteamGlobalAchievement> globalItems)
        {
            foreach (SteamGlobalAchievement item in globalItems)
            {
                if (item.name == apiName)
                {
                    return item.percent;
                }
            }
            return 0m;
        }
    }
}
