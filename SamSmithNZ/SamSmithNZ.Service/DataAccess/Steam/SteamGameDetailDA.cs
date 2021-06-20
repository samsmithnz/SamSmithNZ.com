using Newtonsoft.Json;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamGameDetailDA
    {

        //https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=35D42236AAC777BEDB12CDEB625EF289&appid=200510&l=en&format=xml       
        public async Task<SteamGameDetail> GetDataAsync(string appID)
        {
            SteamGameDetail gameDetail;

            //Make a request to the steam API
            SteamGameDetailDA da = new();
            string jsonRequestString = "https://api.steampowered.com/ISteamUserStats/GetSchemaForGame/v0002/?key=" + Utility.MySteamWebAPIKey + "&appid=" + appID.ToString() + "&l=en";
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonResult == "{\n\t\"game\": {\n\n\t}\n}")
            {
                gameDetail = null;
            }
            else
            {
                gameDetail = JsonConvert.DeserializeObject<SteamGameDetail>(jsonResult);
            }

            return gameDetail;
        }

    }
}
