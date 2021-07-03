using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class MatchupViewModel
    {
        public MatchupViewModel(TeamStatistics team1Statistics, TeamStatistics team2Statistics, List<Game> games)
        {
            this.Team1Statistics = team1Statistics;
            this.Team2Statistics = team2Statistics;
            this.Games = games;
        }

        public TeamStatistics Team1Statistics { get; set; }
        public TeamStatistics Team2Statistics { get; set; }
        public List<Game> Games { get; set; }

    }
}