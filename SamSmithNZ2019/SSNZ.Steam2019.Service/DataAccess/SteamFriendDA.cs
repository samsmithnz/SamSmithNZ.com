using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam2019.Service.Models;
using System.Net;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public class SteamFriendDA
    {

        //http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend&format=xml      
        public async Task<SteamFriendList> GetDataAsync(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&relationship=friend";
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            //If the json returned a few new lines, return null, the player wasn't found
            if (jsonResult == "{\n\n}")
            {
                return null;
            }
            else
            {
                SteamFriendList result = JsonConvert.DeserializeObject<SteamFriendList>(jsonResult);
                return result;
            }

        }     
    }
}
