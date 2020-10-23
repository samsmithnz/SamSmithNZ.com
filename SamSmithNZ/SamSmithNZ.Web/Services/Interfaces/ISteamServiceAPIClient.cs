using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface ISteamServiceAPIClient
    {
        Task<Player> GetPlayer(string steamID, bool useCache);
        Task<List<Game>> GetPlayerGames(string steamID, bool useCache);
        Task<GameDetail> GetGameDetail(string steamID, string appID, bool useCache);
        Task<List<Friend>> GetFriends(string steamID, bool useCache);
    }
}
