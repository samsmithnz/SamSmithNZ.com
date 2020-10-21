using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;

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
        public async Task<List<TournamentTopGoalScorer>> GetTournamentTopGoalScorers(int tournamentCode)
        {
            return await _repo.GetList(tournamentCode);
        }
    }
}
