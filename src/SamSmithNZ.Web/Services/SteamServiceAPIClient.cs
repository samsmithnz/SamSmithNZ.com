using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.Steam;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services
{
    public class SteamServiceApiClient : BaseServiceApiClient, ISteamServiceApiClient
    {
        private readonly IConfiguration _configuration;

        public SteamServiceApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            HttpClient client = new()
            {
                BaseAddress = new(_configuration["AppSettings:WebServiceURL"])
            };
            base.SetupClient(client);
        }

        public async Task<Player> GetPlayer(string steamID)
        {
            Uri url = new($"api/Steam/Player/GetPlayer?SteamID=" + steamID, UriKind.Relative);
            Player result = await base.ReadMessageItem<Player>(url);
            if (result == null)
            {
                return new Player();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<Game>> GetPlayerGames(string steamID)
        {
            Uri url = new($"api/Steam/PlayerGames/GetPlayerGames?SteamID=" + steamID, UriKind.Relative);
            List<Game> results = await base.ReadMessageList<Game>(url);
            if (results == null)
            {
                return new();
            }
            else
            {
                return results;
            }
        }

        public async Task<GameDetail> GetGameDetail(string steamID, string appID)
        {
            Uri url = new($"api/Steam/GameDetails/GetGameDetails?SteamID=" + steamID + "&appID=" + appID, UriKind.Relative);
            GameDetail result = await base.ReadMessageItem<GameDetail>(url);
            if (result == null)
            {
                return new();
            }
            else
            {
                return result;
            }
        }

        public async Task<List<Friend>> GetFriends(string steamID)
        {
            Uri url = new($"api/Steam/Friends/GetFriends?SteamID=" + steamID, UriKind.Relative);
            List<Friend> result = await base.ReadMessageList<Friend>(url);
            if (result == null)
            {
                return new List<Friend>();
            }
            else
            {
                return result;
            }
        }
    }
}
