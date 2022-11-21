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
            //Update the current team ELO ratings, saving them in a dictonary for part 2
            List<TeamELORating> teams = await _repo.CalculateEloForTournamentAsync(tournamentCode);
            Dictionary<int, int> teamEloRatins = new();
            foreach (TeamELORating team in teams)
            {
                await _repo.SaveTeamELORatingAsync(team.TournamentCode, team.TeamCode, team.ELORating);
                if (teamEloRatins.ContainsKey(team.TeamCode))
                {
                    teamEloRatins[team.TeamCode] = team.ELORating;
                }
                else
                {
                    teamEloRatins.Add(team.TeamCode, team.ELORating);
                }
            }
            //Update the ELO ratings for each game
            List<Game> games = await _gamesRepo.GetListByTournament(tournamentCode);
            foreach (Game game in games)
            {
                game.Team1PreGameEloRating = teamEloRatins[game.Team1Code];
                game.Team2PreGameEloRating = teamEloRatins[game.Team2Code];
                await _gamesRepo.SaveItem(game);
            }
            return true;
        }
    }
}
