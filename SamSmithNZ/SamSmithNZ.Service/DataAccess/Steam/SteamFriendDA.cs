using Newtonsoft.Json;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamFriendDA
    {

        //https://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend&format=xml      
        public async Task<SteamFriendList> GetDataAsync(string steamID)
        {
            SteamFriendList friendList;

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

            return friendList;

        }
    }
}
