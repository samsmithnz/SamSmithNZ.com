using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameDataAccess _repo;

        public GameController(IGameDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetGames")]
        public async Task<List<Game>> GetGames(int tournamentCode, int roundNumber, string roundCode, bool includeGoals)
        {
            return await _repo.GetList(tournamentCode, roundNumber, roundCode, includeGoals);
        }

        [HttpGet("GetGamesByTeam")]
        public async Task<List<Game>> GetGamesByTeam(int teamCode)
        {
            return await _repo.GetListByTeam(teamCode);
        }

        [HttpGet("GetPlayoffGames")]
        public async Task<List<Game>> GetPlayoffGames(int tournamentCode, int roundNumber, bool includeGoals)
        {
            return await _repo.GetListByPlayoff(tournamentCode, roundNumber, includeGoals);
        }

        [HttpGet("GetGame")]
        public async Task<Game> GetGame(int gameCode)
        {
            return await _repo.GetItem(gameCode);
        }
    }
}
