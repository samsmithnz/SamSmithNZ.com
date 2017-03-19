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
    public class SteamGameDetailDA
    {
        //http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Global.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
            WebClient newClient = new WebClient();
            newClient.Encoding = UTF8Encoding.UTF8;
            string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            
            SteamGameDetail result = JsonConvert.DeserializeObject<SteamGameDetail>(jsonData);
            return result;
        }

    }
}
