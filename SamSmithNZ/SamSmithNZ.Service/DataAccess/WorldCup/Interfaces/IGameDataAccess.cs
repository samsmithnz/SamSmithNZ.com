using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGameDataAccess
    {

        Task<List<Game>> GetList(int tournamentCode, int roundNumber, string roundCode);
        Task<List<Game>> GetListByTeam(int teamCode);
        Task<List<Game>> GetListByPlayoff(int tournamentCode, int roundNumber);
        Task<List<Game>> GetListByTournament(int tournamentCode);
        Task<Game> GetItem(int gameCode);
        Task<bool> SaveItem(Game game);

    }
}