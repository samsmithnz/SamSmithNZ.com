using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.Models;

namespace SSNZ.Steam.Data
{
    public class SteamPlayerAchievementsForAppDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=200510&key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&l=en&format=xml
        public async Task<Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>> GetDataAsync(string steamID, string appID)
        {

            string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            //If the Json returned an error, process it into a AppError object
            if (jsonData.IndexOf("{\n\t\"playerstats\": {\n\t\t\"error\"") >= 0)
            {
                SteamPlayerAchievementsForAppError errorResult = JsonConvert.DeserializeObject<SteamPlayerAchievementsForAppError>(jsonData);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(null, errorResult);
            }
            else
            {
                SteamPlayerAchievementsForApp result = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonData);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(result, null);
            }
        }

        public Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError> GetDataOld(string steamID, string appID)
        {

            string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetPlayerAchievements/v0001/?appid=" + appID.ToString() + "&key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&l=en";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData = Utility.GetPageAsStringOld(new Uri(jsonRequestString));

            //If the Json returned an error, process it into a AppError object
            if (jsonData.IndexOf("{\n\t\"playerstats\": {\n\t\t\"error\"") >= 0)
            {
                SteamPlayerAchievementsForAppError errorResult = JsonConvert.DeserializeObject<SteamPlayerAchievementsForAppError>(jsonData);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(null, errorResult);
            }
            else
            {
                SteamPlayerAchievementsForApp result = JsonConvert.DeserializeObject<SteamPlayerAchievementsForApp>(jsonData);
                return new Tuple<SteamPlayerAchievementsForApp, SteamPlayerAchievementsForAppError>(result, null);
            }
        }

    }
}
