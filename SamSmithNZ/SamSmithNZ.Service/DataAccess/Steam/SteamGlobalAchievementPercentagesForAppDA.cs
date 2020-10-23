using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;
using System.Net;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(IRedisService redisService, string appID, bool useCache)
        {
            SteamGlobalAchievementsForApp globalAchievementsForApp = null;
            string cacheKeyName = "globalAchievements-" + appID;
            TimeSpan cacheExpirationTime = new TimeSpan(24, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                globalAchievementsForApp = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                globalAchievementsForApp = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonResult);
                //set the cache with the updated record
                if (redisService != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(globalAchievementsForApp), cacheExpirationTime);
                }
            }
            return globalAchievementsForApp;
        }
    }
}
