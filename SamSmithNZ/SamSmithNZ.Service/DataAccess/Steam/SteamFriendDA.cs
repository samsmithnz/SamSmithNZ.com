using Newtonsoft.Json;
using SamSmithNZ.Service.DataAccess.Steam.Interfaces;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamFriendDA
    {

        //https://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend&format=xml      
        public async Task<SteamFriendList> GetDataAsync(IRedisService redisService, string steamID, bool useCache)
        {
            SteamFriendList friendList;
            string cacheKeyName = "friendList-" + steamID;
            TimeSpan cacheExpirationTime = new(12, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null && useCache == true)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                friendList = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamFriendList>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "https://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&relationship=friend";
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                //If the json returned a few new lines, return null, the player wasn't found
                if (jsonResult == "{\n\n}" || jsonResult == "{}")
                {
                    friendList = null;
                }
                else
                {
                    friendList = JsonConvert.DeserializeObject<SteamFriendList>(jsonResult);
                }

                //set the cache with the updated record
                if (redisService != null)
                {
                    await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(friendList), cacheExpirationTime);
                }
            }

            return friendList;

        }
    }
}
