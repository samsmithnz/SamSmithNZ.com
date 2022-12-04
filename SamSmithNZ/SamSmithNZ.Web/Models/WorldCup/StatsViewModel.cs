using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class StatsViewModel
    {
        public int TournamentCode { get; set; }
        public List<StatsAverageTournamentGoals> StatsAverageTournamentGoalsList { get; set; }
    }
}
