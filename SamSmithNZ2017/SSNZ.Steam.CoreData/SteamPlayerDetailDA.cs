using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SSNZ.Steam.CoreModels;
using System.Net;

namespace SSNZ.Steam.CoreData
{
    public class SteamPlayerDetailDA
    {
        //http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public async Task<SteamPlayerDetail> GetDataAsync(string commaSeperatedSteamIDs)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Global.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            SteamPlayerDetail result = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonData);
            return result;
        }
      
    }
}
