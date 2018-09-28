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

        //http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(IRedisService redisService, string appID)
        {
            SteamGameDetail gameDetail = null;
            string gameDetailRedisName = "gameDetail-" + appID;
            TimeSpan cacheExpirationTime = new TimeSpan(0, 0, 30);

            //Get the details of the game
            string gameDetailJSON = null;

            //Check the cache
            if (redisService != null)
            {
                gameDetailJSON = await redisService.GetAsync(gameDetailRedisName);
            }
            if (gameDetailJSON != null)
            {
                gameDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamGameDetail>(gameDetailJSON);
            }
            else
            {
                //Make a request to the steam API
                SteamGameDetailDA da = new SteamGameDetailDA();
                string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Utility.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
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
                await redisService.SetAsync(gameDetailRedisName, Newtonsoft.Json.JsonConvert.SerializeObject(gameDetail), cacheExpirationTime);
            }
            return gameDetail;
        }

    }
}
