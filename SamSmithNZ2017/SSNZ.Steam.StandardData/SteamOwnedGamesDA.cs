using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.StandardModels;
using System.Net;

namespace SSNZ.Steam.StandardData
{
    public class SteamOwnedGamesDA
    {

        //http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&include_appinfo=1&format=xml        
        public async Task<SteamOwnedGames> GetDataAsync(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonData == "{\n\t\"response\": {\n\n\t}\n}")
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<SteamOwnedGames>(jsonData);
            }
        }

        public SteamOwnedGames GetDataOld(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData =  Utility.GetPageAsStringOld(new Uri(jsonRequestString));

            if (jsonData == "{\n\t\"response\": {\n\n\t}\n}")
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<SteamOwnedGames>(jsonData);
            }
        }
    }
}
