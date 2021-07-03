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

        [HttpGet("GetTeamMatchup")]
        public async Task<TeamMatchup> GetTeamMatchup(int team1Code, int team2Code)
        {
            TeamMatchup teamMatchup = new();

            //Get team 1 statistics
            Team team1 = await _teamRepo.GetItem(team1Code);
            List<Game> team1Games = await _gameRepo.GetListByTeam(team1Code);
            TeamStatistics team1Statistics = new();
            team1Statistics.Team = team1;
            team1Statistics.Games = team1Games;
            teamMatchup.Team1Statistics = team1Statistics;

            //Get team 2 statistics
            Team team2 = await _teamRepo.GetItem(team2Code);
            List<Game> team2Games = await _gameRepo.GetListByTeam(team2Code);
            TeamStatistics team2Statistics = new();
            team1Statistics.Team = team2;
            team1Statistics.Games = team2Games;
            teamMatchup.Team2Statistics = team2Statistics;

            //Get the games that both teams were part of
            teamMatchup.Games = team1Games;

            return teamMatchup;
        }
    }
}
