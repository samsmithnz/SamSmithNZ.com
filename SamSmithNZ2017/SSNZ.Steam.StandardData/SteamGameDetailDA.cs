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
    public class SteamGameDetailDA
    {
        //http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Global.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = await newClient.DownloadStringTaskAsync(jsonRequestString);
            string jsonData = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonData == "{\n\t\"game\": {\n\n\t}\n}")
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<SteamGameDetail>(jsonData);
            }
        }

        public SteamGameDetail GetDataOld(string appID)
        {
            string jsonRequestString = "http://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Global.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
            //WebClient newClient = new WebClient();
            //newClient.Encoding = UTF8Encoding.UTF8;
            //string jsonData = newClient.DownloadString(jsonRequestString);
            string jsonData = Utility.GetPageAsStringOld(new Uri(jsonRequestString));

            if (jsonData == "{\n\t\"game\": {\n\n\t}\n}")
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<SteamGameDetail>(jsonData);
            }
        }

    }
}
