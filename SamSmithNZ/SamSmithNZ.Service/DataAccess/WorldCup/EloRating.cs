using SamSmithNZ.Service.Models.WorldCup;
using System;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class EloRating
    {
        //These never change for me - so constants they are.
        private const double _diff = 400;
        private const double _kFactor = 32;

        /// <summary>
        ///  Updates the scores in the matchup object. 
        /// </summary>
        /// <param name="team1Score">The team 1 ELO rating</param>
        /// <param name="team2Score">The team 2 ELO rating</param>
        /// <param name="user1WonMatch">Whether team 1 was the winner (if both team 1 and team 2 are false, it's a draw)</param>
        /// <param name="user2WonMatch">Whether team 2 was the winner</param>
        /// <param name="diff">The desired Diff, currently a constant of 400</param>
        /// <param name="kFactor">The K factor. My K factor currently takes into account the type of game and the goals difference</param>
        /// <returns></returns>
        public (int, int) GetEloRatingScoresForMatchUp(int team1Score, int team2Score,
            bool team1Won, bool team2Won,
            double diff = _diff, double kFactor = _kFactor)
        {
            // player A rating
            double user1ScoreDouble = Convert.ToDouble(team1Score);
            // player B rating
            double user2ScoreDouble = Convert.ToDouble(team2Score);

            //expected score for player A = 1 / (1 + 10 ^ ((player A rating - player B rating) / 400 ))
            double player1ExpectedScore = 1d / Convert.ToDouble(1d + Math.Pow(10d, ((user2ScoreDouble - user1ScoreDouble) / diff)));
            //expected score for player B = 1 / (1 + 10 ^ ((player B rating - player A rating) / 400 ))
            double player2ExpectedScore = 1d / Convert.ToDouble(1d + Math.Pow(10d, ((user1ScoreDouble - user2ScoreDouble) / diff)));

            double user1Result = 0;
            double user2Result = 0;
            if (team1Won == true)
            {
                user1Result = 1;
            }
            else if (team2Won == true)
            {
                user2Result = 1;
            }
            else //split the result evenly for a draw
            {
                user1Result = 0.5;
                user2Result = 0.5;
            }

            //player new rating = [player A current rating] + [K Factor] ( userResult - playerExpectedScore )
            int team1Result = Convert.ToInt32(Math.Round(Convert.ToDouble(team1Score) + kFactor * (user1Result - player1ExpectedScore)));
            int team2Result = Convert.ToInt32(Math.Round(Convert.ToDouble(team2Score) + kFactor * (user2Result - player2ExpectedScore)));

            return new(team1Result, team2Result);
        }

        public double CalculateKFactor(Game item)
        {
            double kFactor;
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
                goals *= -1; //the same as games = goals * -1
            }
            if (goals == 2)
            {
                kFactor *= 1.5d;
            }
            else if (goals == 3)
            {
                kFactor *= 2d;
            }
            else if (goals >= 4)
            {
                kFactor *= 3.5d;// (1.75d + ((Convert.ToDouble(goals) - 3d) / 8d));
            }

            ////K factor is then adjusted for total goals scored - if teams can score goals, they can get results.
            //if (item.Team1TotalGoals != null && item.Team2TotalGoals != null)
            //{
            //    kFactor = kFactor + (((int)item.Team1TotalGoals + (int)item.Team2TotalGoals));
            //}

            return kFactor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>1 if team 1 won, 2 if team 2 won, 0 if draw</returns>
        public WhoWonEnum? WhoWon(Game item)
        {
            int? goals = CalculateGoalDifference(item);
            if (goals == null)
            {
                return null; //the game hasn't started yet
            }
            else if (goals > 0)
            {
                return WhoWonEnum.Team1;
            }
            else if (goals < 0)
            {
                return WhoWonEnum.Team2;
            }
            else
            {
                //it was a draw, return 0;
                return WhoWonEnum.Draw;
            }
        }

        public enum WhoWonEnum
        {
            Team1 = 1,
            Team2 = 2,
            Draw = 0
        }

        public int? CalculateGoalDifference(Game item)
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
                    team1Score += item.Team1NormalTimeScore;
                    team2Score += item.Team2NormalTimeScore;
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
    }
}
