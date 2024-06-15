using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/worldcup/[controller]")]
    [ApiController]
    public class OddsController : ControllerBase
    {
        private readonly TournamentTeamDataAccess _dataAccess;

        public OddsController(TournamentTeamDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet("GetOddsForTournament")]
        public async Task<List<Odds>> GetOddsForTournament(int tournamentCode)
        {
            // Implement odds calculation logic based on team ELO ratings and historical performance
            // This is a placeholder for the actual implementation
            return await _dataAccess.GetOddsForTournament(tournamentCode);
        }
    }
}
