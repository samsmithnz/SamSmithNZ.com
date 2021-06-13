using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class ELORatingController : ControllerBase
    {
        private readonly IEloRatingDataAccess _repo;

        public ELORatingController(IEloRatingDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("RefreshTournamentELORatings")]
        public async Task<bool> RefreshTournamentELORatings(int tournamentCode)
        {
            List<TeamELORating> teams = await _repo.CalculateEloForTournamentAsync(tournamentCode);
            foreach (TeamELORating team in teams)
            {
                await _repo.SaveTeamELORatingAsync(team.TournamentCode, team.TeamCode, team.ELORating);               
            }
            return true;
        }
    }
}
