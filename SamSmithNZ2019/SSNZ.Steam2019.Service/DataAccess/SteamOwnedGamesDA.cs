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
    public class SteamOwnedGamesDA
    {

        //http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&include_appinfo=1&format=xml        
        public async Task<SteamOwnedGames> GetDataAsync(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonResult == "{\n\t\"response\": {\n\n\t}\n}")
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<SteamOwnedGames>(jsonResult);
            }
        }

    }
}
