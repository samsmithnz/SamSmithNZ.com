using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class TournamentTopGoalScorerController : ApiController
    {
        public async Task<List<TournamentTopGoalScorer>> GetTournamentTopGoalScorers(int tournamentCode)
        {
            TournamentTopGoalScorerDataAccess da = new TournamentTopGoalScorerDataAccess();
            return await da.GetListAsync(tournamentCode);
        }
    }
}
