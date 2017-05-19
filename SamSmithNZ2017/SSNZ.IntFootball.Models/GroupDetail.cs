using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class GroupDetail
    {
        public GroupDetail() { }

        public string TeamName { get; set; }
        public string TeamFlagName { get; set; }
        public short RoundNumber { get; set; }
        public string RoundCode { get; set; }
        public short TournamentCode { get; set; }
        public short TeamCode { get; set; }
        public short Played { get; set; }
        public short Wins { get; set; }
        public short Draws { get; set; }
        public short Losses { get; set; }
        public short GoalsFor { get; set; }
        public short GoalsAgainst { get; set; }
        public short GoalDifference { get; set; }
        public short Points { get; set; }
        public bool HasQualifiedForNextRound { get; set; }
        public short GroupRanking { get; set; }
        
    }
}
