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
    public class SteamPlayerDetailDA
    {
        //https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public async Task<SteamPlayerDetail> GetDataAsync(IRedisService redisService, string commaSeperatedSteamIDs, bool useCache)
        {
            SteamPlayerDetail playerDetail = null;
            string cacheKeyName = "playerDetail-" + commaSeperatedSteamIDs;
            TimeSpan cacheExpirationTime = new TimeSpan(8, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                playerDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamPlayerDetail>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Utility.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                playerDetail = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonResult);
                //set the cache with the updated record
                if (redisService != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(playerDetail), cacheExpirationTime);
                }
            }
            return playerDetail;
        }
    }
}
