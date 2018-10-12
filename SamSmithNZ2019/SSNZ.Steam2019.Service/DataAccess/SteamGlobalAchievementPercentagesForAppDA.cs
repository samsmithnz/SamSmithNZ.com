using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam2019.Service.Models;
using System.Net;
using SSNZ.Steam2019.Service.Services;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(IRedisService redisService, string appID)
        {
            SteamGlobalAchievementsForApp globalAchievementsForApp = null;
            string cacheKeyName = "globalAchievements-" + appID;
            TimeSpan cacheExpirationTime = new TimeSpan(24, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                globalAchievementsForApp = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                globalAchievementsForApp = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonResult);
                //set the cache with the updated record
                await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(globalAchievementsForApp), cacheExpirationTime);
            }
            return globalAchievementsForApp;
        }
    }
}
