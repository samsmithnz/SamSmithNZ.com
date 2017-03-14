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

            SteamOwnedGamesDA da3 = new SteamOwnedGamesDA();
            SteamOwnedGames gameOwnedGames = da3.GetData(steamID);
            GameDetail result = new GameDetail();
            foreach (Message item in gameOwnedGames.response.games)
            {
                if (item.appid == appID)
                {
                    result.AppID = appID;
                    result.GameName = item.name;
                    result.IconURL = item.img_icon_url;
                    result.LogoURL = item.img_logo_url;
                    break;
                }
            }

            
            GameAchievementsDA da2 = new GameAchievementsDA();
            result.Achievements = da2.GetData(steamID, appID, gameDetail);

            //Calculate the total achieved items
            int totalAchieved = 0;
            foreach (Achievement item in result.Achievements)
            {
                if (item.Achieved == 1)
                {
                    totalAchieved++;
                }
            }
            result.TotalAchieved = totalAchieved;
            if (result.Achievements.Count > 0)
            {
                result.PercentAchieved = (decimal)totalAchieved / (decimal)result.Achievements.Count;
            }

            return result;
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
