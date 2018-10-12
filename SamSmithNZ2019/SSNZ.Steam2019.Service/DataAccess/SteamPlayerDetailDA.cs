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
    public class SteamPlayerDetailDA
    {
        //http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public async Task<SteamPlayerDetail> GetDataAsync(IRedisService redisService, string commaSeperatedSteamIDs)
        {
            SteamPlayerDetail playerDetail = null;
            string cacheKeyName = "playerDetail-" + commaSeperatedSteamIDs;
            TimeSpan cacheExpirationTime = new TimeSpan(8, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                playerDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamPlayerDetail>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Utility.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                playerDetail = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonResult);
                //set the cache with the updated record
                await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(playerDetail), cacheExpirationTime);
            }
            return playerDetail;
        }
    }
}
