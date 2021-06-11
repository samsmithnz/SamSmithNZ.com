using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class TournamentViewModel
    {
        public Tournament Tournament { get; set; }
        public bool ChanceToWin { get; set; }
        public bool IsPlacingTeams { get; set; }
        public List<TournamentTeam> Teams { get; set; }
        public List<TournamentTopGoalScorer> Goals { get; set; }
    }
}
