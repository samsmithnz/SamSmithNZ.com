using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam2019.Service.Models;

namespace SSNZ.Steam2019.Service.DataAccess
{
    public class SteamPlayerAchievementsForAppDA
    {

        //http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=200510&key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&l=en&format=xml
        public async Task<Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>> GetDataAsync(string steamID, string appID)
        {

            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            //If the Json returned an error, process it into a AppError object
            if (jsonResult.IndexOf("{\n\t\"playerstats\": {\n\t\t\"error\"") >= 0)
            {
                SteamPlayerAchievementsForAppError errorResult = JsonConvert.DeserializeObject<SteamPlayerAchievementsForAppError>(jsonResult);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(null, errorResult);
            }
            else
            {
                SteamPlayerAchievementsForApp result = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonResult);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(result, null);
            }
        }

    }
}
