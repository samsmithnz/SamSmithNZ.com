using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IPlayerDataAccess 
    {
        Task<List<Player>> GetList(int gameCode);
        Task<List<Player>> GetPlayerByTournament(int tournamentCode, string playerName);
        Task<bool> SaveItem(Player player);
    }
}