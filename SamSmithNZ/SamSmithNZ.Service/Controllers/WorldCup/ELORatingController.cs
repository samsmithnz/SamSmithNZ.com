using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class EloRatingController : ControllerBase
    {
        private readonly IEloRatingDataAccess _repo;
        
        public EloRatingController(IEloRatingDataAccess repo, IGameDataAccess gamesRepo)
        {
            _repo = repo;
        }

        [HttpGet("RefreshTournamentELORatings")]
        public async Task<bool> RefreshTournamentELORatings(int tournamentCode)
        {
            //Update the target tournament ELO ratings
            return await _repo.UpdateTournamentELORatings(tournamentCode);
        }
    }
}
