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
    public class SteamGameDetailDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(IRedisService redisService, string appID, bool useCache)
        {
            SteamGameDetail gameDetail = null;
            string cacheKeyName = "gameDetail-" + appID;
            TimeSpan cacheExpirationTime = new TimeSpan(24, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                gameDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamGameDetail>(cachedJSON);
            }
            else
            {
                //Make a request to the steam API
                SteamGameDetailDA da = new SteamGameDetailDA();
                string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Utility.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                if (jsonResult == "{\n\t\"game\": {\n\n\t}\n}")
                {
                    gameDetail = null;
                }
                else
                {
                    gameDetail = JsonConvert.DeserializeObject<SteamGameDetail>(jsonResult);
                }
                //set the cache with the updated record
                await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(gameDetail), cacheExpirationTime);
            }
            return gameDetail;
        }

    }
}
