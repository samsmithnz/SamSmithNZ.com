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
 public   class SteamPlayerAchievementsForAppDA
    {
        //http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=200510&key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&l=en&format=xml
        public SteamPlayerAchievementsForApp GetData(string appID, string steamID)
        {
            
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
            string jsonData = new WebClient().DownloadString(jsonRequestString);

            SteamPlayerAchievementsForApp result = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonData);
            return result;
        }
    }
}
