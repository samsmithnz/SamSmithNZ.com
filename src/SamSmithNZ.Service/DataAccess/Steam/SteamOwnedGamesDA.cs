using Newtonsoft.Json;
using SamSmithNZ.Service.Models.Steam;
using System;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.Steam
{
    public class SteamOwnedGamesDA
    {

        //https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=35D42236AAC777BEDB12CDEB625EF289&steamid=76561197971691578&include_appinfo=1&format=xml        
        public async Task<SteamOwnedGames> GetDataAsync(string steamID)
        {
            SteamOwnedGames ownedGames;

            string jsonRequestString = "https://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key=" + Utility.MySteamWebAPIKey + "&steamid=" + steamID + "&include_appinfo=1";
            string jsonResult = await Utility.GetPageAsStringAsync(new Uri(jsonRequestString));

            if (jsonResult == "{\n\t\"response\": {\n\n\t}\n}" || jsonResult == "<html>\n<head>\n<title>500 Internal Server Error</title>\n</head>\n<body>\n<h1>Internal Server Error</h1>\n</body>\n</html>")
            {
                ownedGames = null;
            }
            else
            {
                ownedGames = JsonConvert.DeserializeObject<SteamOwnedGames>(jsonResult);
            }

            return ownedGames;
        }

    }
}
