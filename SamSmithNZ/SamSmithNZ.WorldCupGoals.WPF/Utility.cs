using SamSmithNZ.Service.Models.WorldCup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.WorldCupGoals.WPF
{
    public static class Utility
    {
        public static string GetGameScore(Game game)
        {
            string scoreText;

            if (game.Team1PenaltiesScore != null)
            {
                scoreText = (game.Team1NormalTimeScore + game.Team1ExtraTimeScore).ToString() + " - " + (game.Team2NormalTimeScore + game.Team2ExtraTimeScore).ToString() + " (" + game.Team1PenaltiesScore + " - " + game.Team2PenaltiesScore + " pen)";
            }
            else if (game.Team1ExtraTimeScore != null)
            {
                scoreText = (game.Team1NormalTimeScore + game.Team1ExtraTimeScore).ToString() + " - " + (game.Team2NormalTimeScore + game.Team2ExtraTimeScore).ToString() + " (et)";
            }
            else
            {
                scoreText = game.Team1NormalTimeScore.ToString() + " - " + game.Team2NormalTimeScore.ToString();
            }
            return scoreText;
        }
    }
}
