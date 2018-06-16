using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class Group
    {
        public Group() { }

        public string TeamName { get; set; }
        public string TeamFlagName { get; set; }
        public int RoundNumber { get; set; }
        public string RoundCode { get; set; }
        public int TournamentCode { get; set; }
        public int TeamCode { get; set; }
        public int Played { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public bool HasQualifiedForNextRound { get; set; }
        public int GroupRanking { get; set; }
        public int ELORating { get; set; }


    }
}
