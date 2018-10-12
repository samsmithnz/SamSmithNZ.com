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
    public class SteamFriendDA
    {

        //http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend&format=xml      
        public async Task<SteamFriendList> GetDataAsync(IRedisService redisService, string steamID)
        {
            SteamFriendList friendList = null;
            string cacheKeyName = "friendList-" + steamID;
            TimeSpan cacheExpirationTime = new TimeSpan(12, 0, 0);

            //Check the cache
            string cachedJSON = null;
            if (redisService != null)
            {
                cachedJSON = await redisService.GetAsync(cacheKeyName);
            }
            if (cachedJSON != null)
            {
                friendList = Newtonsoft.Json.JsonConvert.DeserializeObject<SteamFriendList>(cachedJSON);
            }
            else
            {
                string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&relationship=friend";
                string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

                //If the json returned a few new lines, return null, the player wasn't found
                if (jsonResult == "{\n\n}")
                {
                    friendList = null;
                }
                else
                {
                    friendList = JsonConvert.DeserializeObject<SteamFriendList>(jsonResult);
                }
                //set the cache with the updated record
                await redisService.SetAsync(cacheKeyName, Newtonsoft.Json.JsonConvert.SerializeObject(friendList), cacheExpirationTime);
            }

            return friendList;

        }
    }
}
