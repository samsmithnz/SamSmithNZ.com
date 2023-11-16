using Newtonsoft.Json;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(string appID)
        {
            SteamGlobalAchievementsForApp globalAchievementsForApp;

            string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));
            globalAchievementsForApp = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonResult);

            return globalAchievementsForApp;
        }
    }
}
