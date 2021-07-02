using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class TeamStatisticsController : ControllerBase
    {
        private readonly ITeamDataAccess _teamRepo;
        private readonly IGameDataAccess _gameRepo;

        public TeamStatisticsController(ITeamDataAccess teamRepo, IGameDataAccess gameRepo)
        {
            _teamRepo = teamRepo;
            _gameRepo = gameRepo;
        }

        [HttpGet("GetTeamStatistics")]
        public async Task<TeamStatistics> GetTeamStatistics(int teamCode)
        {
            Team team = await _teamRepo.GetItem(teamCode);
            List<Game> games = await _gameRepo.GetListByTeam(teamCode);
            TeamStatistics teamStatistics = new();
            teamStatistics.Team = team;
            teamStatistics.Games = games;            
            
            return teamStatistics;
        }
    }
}
