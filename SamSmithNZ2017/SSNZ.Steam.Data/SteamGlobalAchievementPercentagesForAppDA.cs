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
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public SteamGlobalAchievementsForApp GetData(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            string jsonData = new WebClient().DownloadString(jsonRequestString);

            SteamGlobalAchievementsForApp result = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonData);
            return result;
        }
    }
}
