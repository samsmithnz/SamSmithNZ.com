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
    public class SteamPlayerDetailDA
    {
        //http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public SteamPlayerDetail GetData(string commaSeperatedSteamIDs)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Global.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
            string jsonData = new WebClient().DownloadString(jsonRequestString);

            SteamPlayerDetail result = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonData);
            return result;
        }
    }
}
