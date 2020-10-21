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
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentDataAccess _repo;

        public TournamentController(ITournamentDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetTournaments")]
        public async Task<List<Tournament>> GetTournaments(int competitionCode = 1)
        {
            return await _repo.GetList(competitionCode);
        }

        [HttpGet("GetTournament")]
        public async Task<Tournament> GetTournament(int tournamentCode)
        {
            return await _repo.GetItem(tournamentCode);
        }
    }
}
