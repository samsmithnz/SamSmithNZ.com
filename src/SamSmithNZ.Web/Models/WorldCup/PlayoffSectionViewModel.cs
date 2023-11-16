using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffSectionViewModel
    {
        public PlayoffSectionViewModel(List<Game> games, List<Playoff> playoffs, List<string> rounds, bool showDebugElements)
        {
            List<Game> filteredGames = new();
            foreach (Game game in games)
            {
                if (rounds.Contains(game.RoundCode) == true && game.RowType == 1)
                {
                    filteredGames.Add(game);
                }
            }

            List<Playoff> filteredPlayoffs = new();
            foreach (Playoff playoff in playoffs)
            {
                if (rounds.Contains(playoff.RoundCode) == true)
                {
                    filteredPlayoffs.Add(playoff);
                }
            }

            Games = filteredGames;
            Playoffs = filteredPlayoffs;
            this.ShowDebugElements = showDebugElements;
        }

        public bool ShowDebugElements { get; private set; }

        public List<Game> Games { get; set; }
        public List<Playoff> Playoffs { get; set; }

    }
}
