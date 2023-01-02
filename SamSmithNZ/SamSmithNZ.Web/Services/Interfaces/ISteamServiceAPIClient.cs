using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SamSmithNZ.Service.Models.Steam;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface ISteamServiceApiClient
    {
        Task<Player> GetPlayer(string steamID);
        Task<List<Game>> GetPlayerGames(string steamID);
        Task<GameDetail> GetGameDetail(string steamID, string appID);
        Task<List<Friend>> GetFriends(string steamID);
    }
}
