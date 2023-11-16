using System;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class GameGoalAssignment
    {
        public GameGoalAssignment() { }

        public int GameCode { get; set; }
        public int GameNumber { get; set; }
        public string RoundCode { get; set; }
        public DateTime GameTime { get; set; }
        public string Team1Name { get; set; }
        public string Team2Name { get; set; }
        public string Score { get; set; }
        public int TotalGameTableGoals { get; set; }
        public int TotalGoalTableGoals { get; set; }
        public int TotalGameTablePenaltyShootoutGoals { get; set; }
        public int TotalPenaltyShootoutTableGoals { get; set; }

    }
}