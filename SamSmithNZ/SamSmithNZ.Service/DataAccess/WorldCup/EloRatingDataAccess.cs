using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static SamSmithNZ.Service.DataAccess.WorldCup.EloRating;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class EloRatingDataAccess : BaseDataAccess<EloRating>, IEloRatingDataAccess
    {
        private readonly IConfiguration _configuration;
        public EloRatingDataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            base.SetupConnectionString(configuration);
        }

        public async Task<List<TeamELORating>> UpdateTournamentELORatings(int tournamentCode)
        {
            List<TeamELORating> results = await CalculateEloForTournamentAsync(tournamentCode);

            foreach (TeamELORating item in results)
            {
                await SaveTeamELORatingAsync(tournamentCode, item.TeamCode, item.ELORating);
            }

            return results;
        }

        public async Task<List<TeamELORating>> CalculateEloForTournamentAsync(int tournamentCode)
        {
            double diff = 400;
            double kRating = 32;

            GameDataAccess da = new(_configuration);
            List<Game> gameList = await da.GetListByTournament(tournamentCode);

            //TeamDataAccess da2 = new(_configuration);
            //List<Team> teamList = await da2.GetList();

            TournamentTeamDataAccess da3 = new(_configuration);
            List<TournamentTeam> tournamentTeams = await da3.GetQualifiedTeams(tournamentCode);

            //Update all of the tournament team ELO ratings
            foreach (TournamentTeam tournamentTeam in tournamentTeams)
            {
                await SaveTeamELORatingAsync(tournamentCode, tournamentTeam.TeamCode, tournamentTeam.StartingEloRating);
            }
            //refresh tournament ELO ratings
            tournamentTeams = await da3.GetQualifiedTeams(tournamentCode);

            List<TeamELORating> teamRatingList = new();
            foreach (Game game in gameList)
            {
                int? team1StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams);
                int? team2StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams);
                bool gameIsDirty = false;

                //if (game.Team1Code == 10 || game.Team2Code == 10)
                //{
                //    System.Diagnostics.Debug.WriteLine("Game: " + game.GameNumber + ", Team1: " + game.Team1Name + ", Team1Elo: " + game.Team1PreGameEloRating + ", Team2: " + game.Team2Name + ", Team2Elo: " + game.Team2PreGameEloRating);
                //}

                if (game.Team1PreGameEloRating == team1StartingEloRating || game.Team2PreGameEloRating != team2StartingEloRating)
                {
                    game.Team1PreGameEloRating = team1StartingEloRating;
                    game.Team2PreGameEloRating = team2StartingEloRating;
                    gameIsDirty = true;
                }
                //Calculate the ELO rating for each team, adding it to the teamRatingList object if it's not already in there
                //TODO: Change this so that it saves ELO updates PER game, instead of just the final ELO rating
                TeamELORating team1 = GetTeamELORating(tournamentCode, game.Team1Code, game.Team1Name, team1StartingEloRating, teamRatingList);
                TeamELORating team2 = GetTeamELORating(tournamentCode, game.Team2Code, game.Team2Name, team2StartingEloRating, teamRatingList);
                EloRating eloRating = new();
                WhoWonEnum? result = eloRating.WhoWon(game);
                kRating = eloRating.CalculateKFactor(game);
                if (result != null)
                {
                    (int, int) eloResult;
                    //the game has started yet, we can process the game
                    if (result == WhoWonEnum.Team1)
                    {
                        eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, true, false, diff, kRating);
                        team1.Wins++;
                        team2.Losses++;
                    }
                    else if (result == WhoWonEnum.Team2)
                    {
                        eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, false, true, diff, kRating);
                        team1.Losses++;
                        team2.Wins++;
                    }
                    else
                    {
                        eloResult = eloRating.GetEloRatingScoresForMatchUp(team1.ELORating, team2.ELORating, false, false, diff, kRating);
                        team1.Draws++;
                        team2.Draws++;
                    }
                    team1.ELORating = eloResult.Item1;
                    team1.GameCount++;
                    team2.ELORating = eloResult.Item2;
                    team2.GameCount++;
                    if (gameIsDirty == true || game.Team1PostGameEloRating != team1.ELORating || game.Team2PostGameEloRating != team2.ELORating)
                    {
                        game.Team1PostGameEloRating = team1.ELORating;
                        game.Team2PostGameEloRating = team2.ELORating;
                        SetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams, team1.ELORating);
                        SetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams, team2.ELORating);
                        await da.SaveItem(game);
                    }
                }
            }

            //Sort the teas
            teamRatingList.Sort((x, y) => y.ELORating.CompareTo(x.ELORating));

            //Update the team ratings 
            //TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
            //da3.SaveItem();

            return teamRatingList;
        }

        private static int GetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams)
        {
            int result = 0;
            foreach (TournamentTeam item in tournamentTeams)
            {
                if (item.TeamCode == teamCode)
                {
                    result = item.ELORating;
                    break;
                }
            }
            return result;
        }

        private static bool SetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams, int eloRating)
        {
            foreach (TournamentTeam item in tournamentTeams)
            {
                if (item.TeamCode == teamCode)
                {
                    item.ELORating = eloRating;
                    break;
                }
            }
            return true;
        }

        public async Task<bool> SaveTeamELORatingAsync(int tournamentCode, int teamCode, int eloRating)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", teamCode, DbType.Int32);
            parameters.Add("@ELORating", eloRating, DbType.Int32);

            return await base.SaveItem("FB_SaveTournamentTeamELORating", parameters);
        }

        private static TeamELORating GetTeamELORating(int tournamentCode, int teamCode, string teamName, int? currentELORanking, List<TeamELORating> teamList)
        {
            foreach (TeamELORating item in teamList)
            {
                if (item.TeamCode == teamCode)
                {
                    return item;
                }
            }
            if (currentELORanking < 500 || currentELORanking == null)
            {
                currentELORanking = 1000;
            }
            TeamELORating newTeam = new(tournamentCode, teamCode, teamName, (int)currentELORanking);
            teamList.Add(newTeam);
            return newTeam;
        }

    }
}
