using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.Models;
using System.Net;

namespace SSNZ.Steam.Data
{
    public class SteamFriendDA
    {

        //http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&relationship=friend&format=xml      
        public async Task<SteamFriendList> GetDataAsync(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetFriendList/v0001/?key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&relationship=friend";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonData =="{\n\n}")
            {
                return null;
            }
            else
            {
                SteamFriendList result = JsonConvert.DeserializeObject<SteamFriendList>(jsonData);
                return result;
            }
            
        }
    }
}
