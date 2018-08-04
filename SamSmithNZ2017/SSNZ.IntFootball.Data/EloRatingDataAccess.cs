using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SSNZ.IntFootball.Models;

namespace SSNZ.IntFootball.Data
{
    public class EloRatingDataAccess : GenericDataAccess<EloRating>
    {
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

            GameDataAccess da = new GameDataAccess();
            List<Game> gameList = await da.GetListAsyncByTournament(tournamentCode);

            TeamDataAccess da2 = new TeamDataAccess();
            List<Team> teamList = await da2.GetListAsync();

            TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
            List<TournamentTeam> tournamentTeams = await da3.GetQualifiedTeamsAsync(tournamentCode);
            //Update and refresh all of the tournament team ELO ratings
            foreach (TournamentTeam tournamentTeam in tournamentTeams)
            {
                await SaveTeamELORatingAsync(tournamentCode, tournamentTeam.TeamCode, tournamentTeam.StartingEloRating);
            }
            tournamentTeams = await da3.GetQualifiedTeamsAsync(tournamentCode);

            List<TeamELORating> teamRatingList = new List<TeamELORating>();
            foreach (Game game in gameList)
            {
                int? team1StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams);
                int? team2StartingEloRating = GetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams);
                bool gameIsDirty = false;

                if (game.Team1Code == 10 || game.Team2Code == 10)
                {
                    System.Diagnostics.Debug.WriteLine("Game: " + game.GameNumber + ", Team1: " + game.Team1Name + ", Team1Elo: " + game.Team1PreGameEloRating + ", Team2: " + game.Team2Name + ", Team2Elo: " + game.Team2PreGameEloRating);
                }

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
                EloRating.Matchup match = new EloRating.Matchup();
                match.User1Score = team1.ELORating;
                match.User2Score = team2.ELORating;
                int? result = WhoWon(game);
                kRating = CalculateKFactor(game);
                if (result == null)
                {
                    //the game hasn't started yet, do nothing
                }
                else
                {
                    if (result == 1)
                    {
                        EloRating.UpdateEloRatingScores(match, true, false, diff, kRating);
                        team1.Wins++;
                        team2.Losses++;
                    }
                    else if (result == 2)
                    {
                        EloRating.UpdateEloRatingScores(match, false, true, diff, kRating);
                        team1.Losses++;
                        team2.Wins++;
                    }
                    else
                    {
                        EloRating.UpdateEloRatingScores(match, false, false, diff, kRating);
                        team1.Draws++;
                        team2.Draws++;
                    }
                    team1.ELORating = match.User1Score;
                    team1.GameCount++;
                    team2.ELORating = match.User2Score;
                    team2.GameCount++;
                    if (gameIsDirty == true || game.Team1PostGameEloRating != team1.ELORating || game.Team2PostGameEloRating != team2.ELORating)
                    {
                        game.Team1PostGameEloRating = team1.ELORating;
                        game.Team2PostGameEloRating = team2.ELORating;
                        SetTournamentTeamCurrentEloRanking(game.Team1Code, tournamentTeams, team1.ELORating);
                        SetTournamentTeamCurrentEloRanking(game.Team2Code, tournamentTeams, team2.ELORating);
                        await da.SaveItemAsync(game);
                    }
                }
            }

            //Sort the teas
            teamRatingList.Sort((x, y) => y.ELORating.CompareTo(x.ELORating));

            //Update the team ratings 
            //TournamentTeamDataAccess da3 = new TournamentTeamDataAccess();
            //da3.SaveItemAsync();

            return teamRatingList;
        }

        private int GetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams)
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

        private bool SetTournamentTeamCurrentEloRanking(int teamCode, List<TournamentTeam> tournamentTeams, int eloRating)
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
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", teamCode, DbType.Int32);
            parameters.Add("@ELORating", eloRating, DbType.Int32);

            return await base.PostItemAsync("FB_SaveTournamentTeamELORating", parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <returns>1 if team 1 won, 2 if team 2 won, 0 if draw</returns>
        private int? WhoWon(Game item)
        {
            int? goals = CalculateGoalDifference(item);
            if (goals == null)
            {
                return null; //the game hasn't started yet
            }
            else if (goals > 0)
            {
                return 1;
            }
            else if (goals < 0)
            {
                return 2;
            }
            else
            {
                //it was a draw, return 0;
                return 0;
            }
        }

        private int? CalculateGoalDifference(Game item)
        {
            if (item.Team1NormalTimeScore == null || item.Team2NormalTimeScore == null)
            {
                return null;//the game hasn't started yet
            }
            else
            {
                int? team1Score = 0;
                int? team2Score = 0;
                if (item.Team1PenaltiesScore >= 0)
                {
                    team1Score = team1Score + item.Team1NormalTimeScore + item.Team1ExtraTimeScore.GetValueOrDefault() + item.Team1PenaltiesScore.GetValueOrDefault();
                    team2Score = team2Score + item.Team2NormalTimeScore + item.Team2ExtraTimeScore.GetValueOrDefault() + item.Team2PenaltiesScore.GetValueOrDefault();
                    //if (item.Team1PenaltiesScore > item.Team2PenaltiesScore)
                    //{
                    //    return 1;
                    //}
                    //else if (item.Team1PenaltiesScore < item.Team2PenaltiesScore)
                    //{
                    //    return 2;
                    //}
                }
                else if (item.Team1ExtraTimeScore >= 0)
                {
                    team1Score = team1Score + item.Team1NormalTimeScore + item.Team1ExtraTimeScore.GetValueOrDefault();
                    team2Score = team2Score + item.Team2NormalTimeScore + item.Team2ExtraTimeScore.GetValueOrDefault();
                    //if (item.Team1ExtraTimeScore > item.Team2ExtraTimeScore)
                    //{
                    //    return 1;
                    //}
                    //else if (item.Team1ExtraTimeScore < item.Team2ExtraTimeScore)
                    //{
                    //    return 2;
                    //}
                }
                else if (item.Team1NormalTimeScore >= 0)
                {
                    team1Score = team1Score + item.Team1NormalTimeScore;
                    team2Score = team2Score + item.Team2NormalTimeScore;
                    //if (item.Team1NormalTimeScore > item.Team2NormalTimeScore)
                    //{
                    //    return 1;
                    //}
                    //else if (item.Team1NormalTimeScore < item.Team2NormalTimeScore)
                    //{
                    //    return 2;
                    //}
                }
                return (int)team1Score - (int)team2Score;
            }
        }


        private double CalculateKFactor(Game item)
        {
            double kFactor = 0;
            //K is the weight constant for the tournament played:

            //60 for World Cup finals;
            //50 for continental championship finals and major intercontinental tournaments;
            //40 for World Cup and continental qualifiers and major tournaments;
            //30 for all other tournaments;
            //20 for friendly matches.

            //Everything is currently World Cup Games
            kFactor = 100d;

            //K is then adjusted for the goal difference in the game. 
            //It is increased by half if a game is won by two goals, 
            //by 3/4 if a game is won by three goals, 
            //by a whole 1 if a game is won by four or more goals
            ////and by 3/4 + (N-3)/8 if the game is won by four or more goals, where N is the goal difference.
            int? goals = CalculateGoalDifference(item);
            if (goals < 0)
            {
                goals = goals * -1;
            }
            if (goals == 2)
            {
                kFactor = kFactor * 1.5d;
            }
            else if (goals == 3)
            {
                kFactor = kFactor * 2d;
            }
            else if (goals >= 4)
            {
                kFactor = kFactor * 3.5d;// (1.75d + ((Convert.ToDouble(goals) - 3d) / 8d));
            }

            ////K factor is then adjusted for total goals scored - if teams can score goals, they can get results.
            //if (item.Team1TotalGoals != null && item.Team2TotalGoals != null)
            //{
            //    kFactor = kFactor + (((int)item.Team1TotalGoals + (int)item.Team2TotalGoals));
            //}

            return kFactor;
        }

        private TeamELORating GetTeamELORating(int tournamentCode, int teamCode, string teamName, int? currentELORanking, List<TeamELORating> teamList)
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
            TeamELORating newTeam = new TeamELORating(tournamentCode, teamCode, teamName, (int)currentELORanking);
            teamList.Add(newTeam);
            return newTeam;
        }

    }
}
