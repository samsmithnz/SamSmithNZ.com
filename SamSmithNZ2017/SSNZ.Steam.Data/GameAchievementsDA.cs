using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class GameAchievementsDA
    {
        public List<Achievement> GetData(string steamID, string appID, SteamGameDetail steamGameDetails)
        {
            SteamPlayerAchievementsForAppDA da = new SteamPlayerAchievementsForAppDA();
            SteamPlayerAchievementsForApp playerData = da.GetData(steamID, appID);

            List<Achievement> results = new List<Achievement>();
            if (playerData != null)
            {
                SteamGlobalAchievementPercentagesForAppDA da2 = new SteamGlobalAchievementPercentagesForAppDA();
                SteamGlobalAchievementsForApp globalData = da2.GetData(appID);

                if (playerData.playerstats.achievements != null)
                {
                    foreach (SteamPlayerAchievement item in playerData.playerstats.achievements)
                    {
                        Achievement newItem = new Achievement();
                        newItem.ApiName = item.apiname;
                        newItem.Name = item.name;
                        newItem.Description = item.description;
                        newItem.Achieved = item.achieved;
                        newItem.GlobalPercent = GetGlobalAchievement(item.apiname, globalData.achievementpercentages.achievements);
                        if (steamGameDetails != null && steamGameDetails.game != null && steamGameDetails.game.availableGameStats != null && steamGameDetails.game.availableGameStats.achievements != null)
                        {
                            newItem.IconURL = GetIconURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                            newItem.IconGrayURL = GetIconGrayURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                        }
                        newItem.FriendAchieved = 0;

                        results.Add(newItem);
                    }
                }

                results.Sort(delegate (Achievement p1, Achievement p2)
                {
                    return p2.GlobalPercent.CompareTo(p1.GlobalPercent);
                });
            }

            return results;
        }

        private string GetIconURL(string apiName, List<GameAchievement> steamGameDetails)
        {
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    return item.icon;
                }
            }
            return null;
        }

        private string GetIconGrayURL(string apiName, List<GameAchievement> steamGameDetails)
        {
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    return item.icongray;
                }
            }
            return null;
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
