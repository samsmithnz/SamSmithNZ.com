using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class TournamentTopGoalScorer
    {
        public TournamentTopGoalScorer() { }

        public string PlayerName { get; set; }
        public int TeamCode { get; set; }
        public string TeamName { get; set; }
        public string FlagName { get; set; }
        public int GoalsScored { get; set; }
        public bool IsActive { get; set; }

    }
}
