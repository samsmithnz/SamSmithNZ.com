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
  public  class SteamOwnedGamesDA
    {

        //http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&include_appinfo=1&format=xml        
        public OwnedGames GetData(string steamID)
        {
            string jsonRequestString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Global.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
            string jsonData = new WebClient().DownloadString(jsonRequestString);

            OwnedGames result = JsonConvert.DeserializeObject<OwnedGames>(jsonData);
            return result;
        }
    }
}
