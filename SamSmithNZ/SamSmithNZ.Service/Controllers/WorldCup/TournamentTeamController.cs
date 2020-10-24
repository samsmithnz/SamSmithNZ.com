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
    public class TournamentTeamController : ControllerBase
    {
        private readonly ITournamentTeamDataAccess _repo;

        public TournamentTeamController(ITournamentTeamDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTournamentQualifyingTeams")]
        public async Task<List<TournamentTeam>> GetTournamentQualifyingTeams(int tournamentCode)
        {
            return await _repo.GetQualifiedTeams(tournamentCode);
        }

        [HttpGet("GetTournamentPlacingTeams")]
        public async Task<List<TournamentTeam>> GetTournamentPlacingTeams(int tournamentCode)
        {
            return await _repo.GetTeamsPlacingAsync(tournamentCode);
        }

    }
}
