using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class TournamentTopGoalScorerController : ControllerBase
    {
        private readonly ITournamentTopGoalScorerDataAccess _repo;

        public TournamentTopGoalScorerController(ITournamentTopGoalScorerDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTournamentTopGoalScorers")]
        public async Task<List<TournamentTopGoalScorer>> GetTournamentTopGoalScorers(int tournamentCode, bool getOwnGoals)
        {
            return await _repo.GetList(tournamentCode, getOwnGoals);
        }
    }
}
