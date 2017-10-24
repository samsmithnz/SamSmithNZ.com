using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Models
{
    public class Goal
    {
        public Goal() { }

        public int GoalCode { get; set; }
        public int GameCode { get; set; }
        public int PlayerCode { get; set; }
        public string  PlayerName { get; set; }
        public int GoalTime { get; set; }
        public int InjuryTime { get; set; }
        public bool IsPenalty { get; set; }
        public bool IsOwnGoal { get; set; }

    }
}
