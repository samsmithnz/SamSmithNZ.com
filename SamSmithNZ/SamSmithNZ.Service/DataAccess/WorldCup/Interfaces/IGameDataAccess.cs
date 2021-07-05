using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGameDataAccess
    {

        Task<List<Game>> GetList(int tournamentCode, int roundNumber, string roundCode, bool includeGoals);
        Task<List<Game>> GetListByTeam(int teamCode);
        Task<List<Game>> GetListByPlayoff(int tournamentCode, int roundNumber, bool includeGoals);
        Task<List<Game>> GetListByTournament(int tournamentCode);
        Task<Game> GetItem(int gameCode);
        Task<bool> SaveItem(Game game);
        Task<bool> SaveMigrationItem(Game game);

    }
}