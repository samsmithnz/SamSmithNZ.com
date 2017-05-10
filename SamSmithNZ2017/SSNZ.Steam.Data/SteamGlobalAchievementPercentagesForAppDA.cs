﻿using System;
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
        public async Task<SteamGlobalAchievementsForApp> GetDataAsync(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            SteamGlobalAchievementsForApp result = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonData);
            return result;
        }

        public SteamGlobalAchievementsForApp GetDataOld(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetGlobalAchievementPercentagesForApp/v0002/?gameid=" + appID.ToString();
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData =  Utility.GetPageAsStringOld(new Uri(jsonRequestString));

            SteamGlobalAchievementsForApp result = JsonConvert.DeserializeObject<SteamGlobalAchievementsForApp>(jsonData);
            return result;
        }
    }
}
