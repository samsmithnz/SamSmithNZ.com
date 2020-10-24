using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class EloRating
    {


        // The desired Diff
        // The desired KFactor
        //private int r;

        /// <summary>
        ///  Updates the scores in the matchup object. 
        /// </summary>
        /// <param name="matchup">The Matchup to update</param>
        /// <param name="user1WonMatch">Whether User 1 was the winner (if both user 1 and user 2 are false, it's a draw)</param>
        /// <param name="user2WonMatch">Whether User 2 was the winner</param>
        /// <param name="diff">The desired Diff, currently a constant of 400</param>
        /// <param name="kFactor">The K factor. My K factor currently takes into account the type of game and the goals difference</param>
        /// <returns></returns>
        public static Matchup UpdateEloRatingScores(Matchup matchup, bool user1WonMatch, bool user2WonMatch, double diff, double kFactor)
        {
            // player A rating
            double User1ScoreDouble = Convert.ToDouble(matchup.User1Score);
            // player B rating
            double User2ScoreDouble = Convert.ToDouble(matchup.User2Score);

            //expected score for player A = 1 / (1 + 10 ^ ((player A rating - player B rating) / 400 ))
            double player1ExpectedScore = 1d / Convert.ToDouble(1d + Math.Pow(10d, ((User2ScoreDouble - User1ScoreDouble) / diff)));
            //expected score for player B = 1 / (1 + 10 ^ ((player B rating - player A rating) / 400 ))
            double player2ExpectedScore = 1d / Convert.ToDouble(1d + Math.Pow(10d, ((User1ScoreDouble - User2ScoreDouble) / diff)));

            double user1Result = 0;
            double user2Result = 0;
            if (user1WonMatch == true)
            {
                user1Result = 1;
            }
            else if (user2WonMatch == true)
            {
                user2Result = 1;
            }
            else //split the result evenly for a draw
            {
                user1Result = 0.5;
                user2Result = 0.5;
            }

            //player new rating = [player A current rating] + [K Factor] ( userResult - playerExpectedScore )
            matchup.User1Score = Convert.ToInt32(Math.Round(Convert.ToDouble(matchup.User1Score) + kFactor * (user1Result - player1ExpectedScore)));
            matchup.User2Score = Convert.ToInt32(Math.Round(Convert.ToDouble(matchup.User2Score) + kFactor * (user2Result - player2ExpectedScore)));

            return matchup;
        }

        /// 
        /// Updates the scores in the match, using default Diff and KFactors (400, 100)
        /// 
        /// The Matchup to update
        /// Whether User 1 was the winner (false if User 2 is the winner)
        public static Matchup UpdateScores(Matchup matchup, bool user1WonMatch, bool user2WonMatch)
        {
            return UpdateEloRatingScores(matchup, user1WonMatch, user2WonMatch, 400, 10);
        }

        [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
        public class Matchup
        {
            public int User1Score { get; set; }
            public int User2Score { get; set; }
        }

    }
}
