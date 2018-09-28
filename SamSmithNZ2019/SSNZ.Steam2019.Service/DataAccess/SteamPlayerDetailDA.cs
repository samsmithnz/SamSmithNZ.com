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
    public class SteamPlayerDetailDA
    {

        //http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public async Task<SteamPlayerDetail> GetDataAsync(string commaSeperatedSteamIDs)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Utility.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            SteamPlayerDetail result = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonResult);
            return result;
        }
    }
}
