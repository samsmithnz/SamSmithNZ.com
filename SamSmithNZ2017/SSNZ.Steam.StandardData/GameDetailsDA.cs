using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.Steam.StandardModels;

namespace SSNZ.Steam.StandardData
{
    public class GameDetailsDA
    {
        public async Task<GameDetail> GetDataAsync(string steamID, string appID)
        {
            //Get the details of the game
            SteamGameDetailDA da = new SteamGameDetailDA();
            SteamGameDetail gameDetail = await da.GetDataAsync(appID);

            //Get the list of owned games for the user
            SteamOwnedGamesDA da2 = new SteamOwnedGamesDA();
            SteamOwnedGames gameOwnedGames = await da2.GetDataAsync(steamID);

            //Merge the two datasets into a clean gamedetail object
            GameDetail result = new GameDetail();
            if (gameOwnedGames != null && gameOwnedGames.response != null && gameOwnedGames.response.games != null)
            {
                foreach (Message item in gameOwnedGames.response.games)
                {
                    if (item.appid == appID)
                    {
                        result.AppID = appID;
                        result.GameName = item.name;
                        if (string.IsNullOrEmpty(item.img_icon_url))
                        {
                            result.IconURL = null;
                        }
                        else
                        {
                            result.IconURL = item.img_icon_url;
                        }
                        result.LogoURL = item.img_logo_url;
                        break;
                    }
                }
            }

            //Get the achievements from another dataset
            if (gameDetail != null)
            {
                Tuple<List<Achievement>, string> tempResults = await GetAchievementDataAsync(steamID, appID, gameDetail);
                result.Achievements = tempResults.Item1;
                result.ErrorMessage = tempResults.Item2;

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
            }
            else
            {
                result.Achievements = new List<Achievement>();
            }

            return result;
        }

        public async Task<GameDetail> GetDataWithFriend(string steamID, string appID, string friendSteamId)
        {
            GameDetail details = await GetDataAsync(steamID, appID);
            GameDetail friendDetails = await GetDataAsync(friendSteamId, appID);

            //if the friend details return correctly, (this can fail sometimes if the friend has a private project), then continue:
            if (friendDetails == null || friendDetails.Achievements == null || friendDetails.Achievements.Count == 0)
            {
                details = null;
            }
            else
            {
                details.FriendPercentAchieved = friendDetails.PercentAchieved;
                details.FriendTotalAchieved = friendDetails.TotalAchieved;
                foreach (Achievement item in details.Achievements)
                {
                    //Get the friends achievements for this game
                    Achievement friendItem = FindFriendItem(item.ApiName, friendDetails.Achievements);
                    if (friendItem != null)
                    {
                        item.FriendAchieved = friendItem.Achieved;
                    }
                }
            }

            return details;
        }

        private Achievement FindFriendItem(string apiName, List<Achievement> friendAchievements)
        {
            Achievement result = null;
            foreach (Achievement item in friendAchievements)
            {
                if (item.ApiName == apiName)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        public async Task<Tuple<List<Achievement>, string>> GetAchievementDataAsync(string steamID, string appID, SteamGameDetail steamGameDetails)
        {
            //Get the achievements for the app
            SteamPlayerAchievementsForAppDA da = new SteamPlayerAchievementsForAppDA();
            Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError> playerData = await da.GetDataAsync(steamID, appID);

            List<Achievement> results = new List<Achievement>();
            if (playerData != null && playerData.Item1 != null)
            {
                //Get the global achievement stats for the app
                SteamGlobalAchievementPercentagesForAppDA da2 = new SteamGlobalAchievementPercentagesForAppDA();
                SteamGlobalAchievementsForApp globalData = await da2.GetDataAsync(appID);

                if (playerData.Item1.playerstats.achievements != null)
                {
                    foreach (SteamPlayerAchievement item in playerData.Item1.playerstats.achievements)
                    {
                        //Merge the play achievements with the global and friend achivements to one clean achievement object
                        //if (item.apiname== "DLC4_ACHIEVEMENT_01")
                        //{
                        //    Console.WriteLine("here");
                        //}
                        Achievement newItem = new Achievement();
                        newItem.ApiName = item.apiname;
                        newItem.Name = item.name;
                        newItem.Description = item.description;
                        newItem.IsVisible = true;
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
                            newItem.IsVisible = GetIsVisible(newItem.ApiName, steamGameDetails.game.availableGameStats.achievements);
                        }
                        if (newItem.IsVisible == false && newItem.Achieved == true)
                        {
                            newItem.IsVisible = true;
                        }
                        if (newItem.IsVisible == false)
                        {
                            newItem.Description = "(Note: this is a hidden achievement that has not been released to the public yet)";
                        }
                        newItem.FriendAchieved = false;

                        results.Add(newItem);
                    }
                }

                //Sort the results by global achievement %
                results.Sort(delegate (Achievement p1, Achievement p2)
                {
                    return p2.GlobalPercent.CompareTo(p1.GlobalPercent);
                });
            }

            //Handle any errors that may have been returned from the Steam API
            string error = null;
            if (playerData != null && playerData.Item2 != null && playerData.Item2.playerstats != null && playerData.Item2.playerstats.success == false)
            {
                error = playerData.Item2.playerstats.error;
            }

            return new Tuple<List<Achievement>, string>(results, error);
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

        private bool GetIsVisible(string apiName, List<GameAchievement> steamGameDetails)
        {
            bool result = false;
            foreach (GameAchievement item in steamGameDetails)
            {
                if (item.name == apiName)
                {
                    result = (item.hidden != 1);
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
