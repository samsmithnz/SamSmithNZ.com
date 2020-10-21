using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class TeamELORating
    {
        public TeamELORating(int tournamentCode, int teamCode, string teamName, int eloRating)
        {
            this.TournamentCode = tournamentCode;
            this.TeamCode = teamCode;
            this.TeamName = teamName;
            this.ELORating = eloRating;
        }

        public int TournamentCode { get; set; }
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public int ELORating { get; set; }
        public int GameCount { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

    }
}
