using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSNZ.IntFootball.Models;

namespace SSNZ.IntFootball.Data
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELORatingManager
    {
        //public List<TeamRating> ProcessTeams(List<Game> gameList)
        //{
        //    double diff = 400;
        //    double kRating = 32;

        //    TeamDataAccess da2 = new TeamDataAccess();
        //    List<Team> teamList = da2.GetItems();

        //    List<TeamRating> teamRatingList = new List<TeamRating>();
        //    foreach (Game item in gameList)
        //    {
        //        TeamRating team1 = GetTeamRating(item.Team1Code, item.Team1Name, teamRatingList);
        //        TeamRating team2 = GetTeamRating(item.Team2Code, item.Team2Name, teamRatingList);
        //        EloRating.Matchup match = new EloRating.Matchup();
        //        match.User1Score = team1.Rating;
        //        match.User2Score = team2.Rating;
        //        int result = WhoWon(item);
        //        kRating = CalculateKFactor(item);
        //        if (result == 1)
        //        {
        //            EloRating.UpdateEloRatingScores(match, true, false, diff, kRating);
        //            team1.Wins++;
        //            team2.Losses++;
        //        }
        //        else if (result == 2)
        //        {
        //            EloRating.UpdateEloRatingScores(match, false, true, diff, kRating);
        //            team1.Losses++;
        //            team2.Wins++;
        //        }
        //        else
        //        {
        //            EloRating.UpdateEloRatingScores(match, false, false, diff, kRating);
        //            team1.Draws++;
        //            team2.Draws++;
        //        }
        //        team1.Rating = match.User1Score;
        //        team1.GameCount++;
        //        team2.Rating = match.User2Score;
        //        team2.GameCount++;
        //    }

        //    teamRatingList.Sort((x, y) => y.Rating.CompareTo(x.Rating));
        //    return teamRatingList;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game"></param>
        /// <returns>1 if team 1 won, 2 if team 2 won, 0 if draw</returns>
        //private int WhoWon(Game item)
        //{
        //    int goals = CalculateGoalDifference(item);
        //    if (goals > 0)
        //    {
        //        return 1;
        //    }
        //    else if (goals < 0)
        //    {
        //        return 2;
        //    }
        //    else
        //    {
        //        //it was a draw, return 0;
        //        return 0;
        //    }
        //}

        //private int CalculateGoalDifference(Game item)
        //{
        //    int team1Score = 0;
        //    int team2Score = 0;
        //    if (item.Team1PenaltiesScore >= 0 || item.Team2PenaltiesScore >= 0)
        //    {
        //        team1Score = team1Score + item.Team1PenaltiesScore;
        //        team2Score = team2Score + item.Team2PenaltiesScore;
        //        //if (item.Team1PenaltiesScore > item.Team2PenaltiesScore)
        //        //{
        //        //    return 1;
        //        //}
        //        //else if (item.Team1PenaltiesScore < item.Team2PenaltiesScore)
        //        //{
        //        //    return 2;
        //        //}
        //    }
        //    else if (item.Team1ExtraTimeScore >= 0 || item.Team2ExtraTimeScore >= 0)
        //    {
        //        team1Score = team1Score + item.Team1ExtraTimeScore;
        //        team2Score = team2Score + item.Team2ExtraTimeScore;
        //        //if (item.Team1ExtraTimeScore > item.Team2ExtraTimeScore)
        //        //{
        //        //    return 1;
        //        //}
        //        //else if (item.Team1ExtraTimeScore < item.Team2ExtraTimeScore)
        //        //{
        //        //    return 2;
        //        //}
        //    }
        //    else if (item.Team1NormalTimeScore >= 0 || item.Team2NormalTimeScore >= 0)
        //    {
        //        team1Score = team1Score + item.Team1NormalTimeScore;
        //        team2Score = team2Score + item.Team2NormalTimeScore;
        //        //if (item.Team1NormalTimeScore > item.Team2NormalTimeScore)
        //        //{
        //        //    return 1;
        //        //}
        //        //else if (item.Team1NormalTimeScore < item.Team2NormalTimeScore)
        //        //{
        //        //    return 2;
        //        //}
        //    }
        //    return team1Score - team2Score;
        //}


        //private double CalculateKFactor(Game item)
        //{
        //    double kFactor = 0;
        //    //K is the weight constant for the tournament played:

        //    //60 for World Cup finals;
        //    //50 for continental championship finals and major intercontinental tournaments;
        //    //40 for World Cup and continental qualifiers and major tournaments;
        //    //30 for all other tournaments;
        //    //20 for friendly matches.

        //    //Everything is currently World Cup Games
        //    kFactor = 60d;

        //    //K is then adjusted for the goal difference in the game. 
        //    //It is increased by half if a game is won by two goals, 
        //    //by 3/4 if a game is won by three goals, 
        //    //and by 3/4 + (N-3)/8 if the game is won by four or more goals, where N is the goal difference.
        //    int goals = CalculateGoalDifference(item);
        //    if (goals < 0)
        //    {
        //        goals = goals * -1;
        //    }
        //    if (goals == 2)
        //    {
        //        kFactor = kFactor * 1.5d;
        //    }
        //    else if (goals == 3)
        //    {
        //        kFactor = kFactor * 1.75d;
        //    }
        //    else if (goals >= 4)
        //    {
        //        kFactor = kFactor * (1.75d + ((Convert.ToDouble(goals) - 3d) / 8d));
        //    }

        //    return kFactor;
        //}

        private TeamRating GetTeamRating(short teamCode, string teamName, List<TeamRating> teamList)
        {
            foreach (TeamRating item in teamList)
            {
                if (item.TeamCode == teamCode)
                {
                    return item;
                }
            }
            TeamRating newTeam = new TeamRating(teamCode, teamName, 1000);
            teamList.Add(newTeam);
            return newTeam;
        }

        public int FindTeamELORating(int teamCode, List<TeamRating> teamRatingList)
        {
            foreach (TeamRating item in teamRatingList)
            {
                if (teamCode == item.TeamCode)
                {
                    return item.Rating;
                }
            }
            return 0;
        }

    }
}
