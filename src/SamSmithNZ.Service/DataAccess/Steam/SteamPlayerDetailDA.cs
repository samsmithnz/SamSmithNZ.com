using Newtonsoft.Json;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamPlayerDetailDA
    {
        //https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&steamids=76561197971691578&format=xml
        public async Task<SteamPlayerDetail> GetDataAsync(string commaSeperatedSteamIDs)
        {
            SteamPlayerDetail playerDetail;
            string jsonRequestString = "https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/?key=" + Utility.MySteamWebAPIKey + "&steamids=" + commaSeperatedSteamIDs;
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));
            playerDetail = JsonConvert.DeserializeObject<SteamPlayerDetail>(jsonResult);
            return playerDetail;
        }
    }
}
