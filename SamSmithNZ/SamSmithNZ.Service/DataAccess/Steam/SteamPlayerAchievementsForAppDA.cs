using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamPlayerAchievementsForAppDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=200510&key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&l=en&format=xml
        public async Task<Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>> GetDataAsync(IRedisService redisService, string steamID, string appID, bool useCache)
        {
            SteamPlayerAchievementsForAppError errorResult = null;
            SteamPlayerAchievementsForApp playerAchievements = null;
            string cacheKeyName = "playerAchievements-" + steamID + "-" + appID;
            TimeSpan cacheExpirationTime = new TimeSpan(0, 1, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                playerAchievements = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                //If the Json returned an error, process it into a AppError object
                if (jsonResult.IndexOf("{\n\t\"playerstats\": {\n\t\t\"error\"") >= 0)
                {
                    errorResult = JsonConvert.DeserializeObject<SteamPlayerAchievementsForAppError>(jsonResult);
                    return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(null, errorResult);
                }
                else if (jsonResult == "<html><head><title>Internal Server Error</title></head><body><h1>Internal Server Error</h1>Unknown problem determining WebApi request destination.</body></html>")
                {
                    playerAchievements = null;
                }
                else
                {
                    playerAchievements = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonResult);
                }
                //set the cache with the updated record
                if (redisService != null && playerAchievements != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(playerAchievements), cacheExpirationTime);
                }
            }

            if (errorResult != null)
            {
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(null, errorResult);
            }
            else
            {
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(playerAchievements, null);
            }
        }

    }
}
