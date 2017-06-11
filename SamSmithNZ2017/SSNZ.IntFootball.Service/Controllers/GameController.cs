using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class GameController : ApiController
    {
        public async Task<List<Game>> GetGames(int tournamentCode, int roundNumber, string roundCode)
        {
            GameDataAccess da = new GameDataAccess();
            return await da.GetListAsync(tournamentCode, roundNumber, roundCode);
        }

        public async Task<List<Game>> GetGamesByTeam(int teamCode)
        {
            GameDataAccess da = new GameDataAccess();
            return await da.GetListAsyncByTeam(teamCode);
        }

        public async Task<List<Game>> GetPlayoffGames(int tournamentCode, int roundNumber)
        {
            GameDataAccess da = new GameDataAccess();
            return await da.GetListAsyncByPlayoff(tournamentCode, roundNumber);
        }
    }
}
