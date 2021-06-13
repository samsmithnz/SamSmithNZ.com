using Newtonsoft.Json;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamGameDetailDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(IRedisService redisService, string appID, bool useCache)
        {
            SteamGameDetail gameDetail = null;
            string cacheKeyName = "gameDetail-" + appID;
            TimeSpan cacheExpirationTime = new(24, 0, 0);

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
                SteamGameDetailDA da = new();
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
                if (redisService != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(gameDetail), cacheExpirationTime);
                }
            }
            return gameDetail;
        }

    }
}
