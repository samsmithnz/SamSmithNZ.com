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


            result.Achievements = GetAchievementData(steamID, appID, gameDetail);

            //Calculate the total achieved items
            int totalAchieved = 0;
            foreach (Achievement item in result.Achievements)
            {
                if (item.Achieved == true)
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

        private List<Achievement> GetAchievementData(string steamID, string appID, SteamGameDetail steamGameDetails)
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
                        if (item.achieved == 1)
                        {
                            newItem.Achieved = true;
                        }
                        else
                        {
                            newItem.Achieved = false;
                        }
                        newItem.GlobalPercent = GetGlobalAchievement(item.apiname, globalData.achievementpercentages.achievements);
                        if (steamGameDetails != null && steamGameDetails.game != null && steamGameDetails.game.availableGameStats != null && steamGameDetails.game.availableGameStats.achievements != null)
                        {
                            newItem.IconURL = GetIconURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                            newItem.IconGrayURL = GetIconGrayURL(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                        }
                        newItem.FriendAchieved = false;

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
            string result = null;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = item.icon;
                    break;
                }
            }
            return result;
        }

        private string GetIconGrayURL(string apiName, List<GameAchievement> steamGameDetails)
        {
            string result = null;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = item.icongray;
                    break;
                }
            }
            return result;
        }

        private decimal GetGlobalAchievement(string apiName, List<SteamGlobalAchievement> globalItems)
        {
            decimal result = 0m;
            foreach (SteamGlobalAchievement item in globalItems)
            {
                if (item.name == apiName)
                {
                    result = item.percent;
                    break;
                }
            }
            return result;
        }
    }
}
