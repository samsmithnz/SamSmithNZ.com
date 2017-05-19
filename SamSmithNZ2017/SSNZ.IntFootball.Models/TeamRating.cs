using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Models
{
    public class TeamRating
    {
        public TeamRating(int teamCode, string teamName, int rating)
        {
            this.TeamCode = teamCode;
            this.TeamName = teamName;
            this.Rating = rating;
        }

        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public int Rating { get; set; }
        public int GameCount { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

    }
}
