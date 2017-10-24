using SSNZ.IntFootball.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Goals.WinWPF
{
    public static class Utility
    {
        public static String GetGameScore(Game game)
        {
            String sScore;

            if (game.Team1PenaltiesScore != null)
            {
                sScore = (game.Team1NormalTimeScore + game.Team1ExtraTimeScore).ToString() + " - " + (game.Team2NormalTimeScore + game.Team2ExtraTimeScore).ToString() + " (" + game.Team1PenaltiesScore + " - " + game.Team2PenaltiesScore + " pen)";
            }
            else if (game.Team1ExtraTimeScore != null)
            {
                sScore = (game.Team1NormalTimeScore + game.Team1ExtraTimeScore).ToString() + " - " + (game.Team2NormalTimeScore + game.Team2ExtraTimeScore).ToString() + " (et)";
            }
            else
            {
                sScore = game.Team1NormalTimeScore.ToString() + " - " + game.Team2NormalTimeScore.ToString();
            }
            return sScore;
        }
    }
}
