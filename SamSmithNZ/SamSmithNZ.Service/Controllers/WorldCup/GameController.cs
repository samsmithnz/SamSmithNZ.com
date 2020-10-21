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
    public class GameController : ControllerBase
    {
        private readonly IGameDataAccess _repo;

        public GameController(IGameDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetGames")]
        public async Task<List<Game>> GetGames(int tournamentCode, int roundNumber, string roundCode)
        {
            return await _repo.GetList(tournamentCode, roundNumber, roundCode);
        }

        [HttpGet("GetGamesByTeam")]
        public async Task<List<Game>> GetGamesByTeam(int teamCode)
        {
            return await _repo.GetListByTeam(teamCode);
        }

        [HttpGet("GetPlayoffGames")]
        public async Task<List<Game>> GetPlayoffGames(int tournamentCode, int roundNumber)
        {
            return await _repo.GetListByPlayoff(tournamentCode, roundNumber);
        }

        [HttpGet("GetGame")]
        public async Task<Game> GetGame(int gameCode)
        {
            return await _repo.GetItem(gameCode);
        }
    }
}
