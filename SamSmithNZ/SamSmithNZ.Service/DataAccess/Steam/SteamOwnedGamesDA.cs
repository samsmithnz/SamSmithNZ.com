﻿using System;
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
    public class SteamOwnedGamesDA
    {

        //https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&include_appinfo=1&format=xml        
        public async Task<SteamOwnedGames> GetDataAsync(IRedisService redisService, string steamID, bool useCache)
        {
            SteamOwnedGames ownedGames = null;
            string cacheKeyName = "ownedGames-" + steamID;
            TimeSpan cacheExpirationTime = new TimeSpan(2, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                ownedGames = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamOwnedGames>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                if (jsonResult == "{\n\t\"response\": {\n\n\t}\n}" || jsonResult == "<html>\n<head>\n<title>500 Internal Server Error</title>\n</head>\n<body>\n<h1>Internal Server Error</h1>\n</body>\n</html>")
                {
                    ownedGames = null;
                }
                else
                {
                    ownedGames = JsonConvert.DeserializeObject<SteamOwnedGames>(jsonResult);
                }
                //set the cache with the updated record
                if (redisService != null && ownedGames != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(ownedGames), cacheExpirationTime);
                }
            }
            return ownedGames;
        }

    }
}
