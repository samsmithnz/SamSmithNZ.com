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
        private readonly IGameDataAccess _gamesRepo;

        public ELORatingController(IEloRatingDataAccess repo, IGameDataAccess gamesRepo)
        {
            _repo = repo;
            _gamesRepo = gamesRepo;
        }

        [HttpGet("RefreshTournamentELORatings")]
        public async Task<bool> RefreshTournamentELORatings(int tournamentCode)
        {
            //Update the target tournament ELO ratings
            return await _repo.UpdateTournamentELORatings(tournamentCode);
        }
    }
}
