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
    public class SteamGlobalAchievementPercentagesForAppDA
    {

        //http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=200510
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            SteamGlobalAchievementsForApp result = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonResult);
            return result;
        }
    }
}
