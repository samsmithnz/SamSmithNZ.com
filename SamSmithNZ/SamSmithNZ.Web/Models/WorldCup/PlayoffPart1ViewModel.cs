using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffPart1ViewModel
    {
        public PlayoffPart1ViewModel(List<Game> games, int gameNumber, bool showDebugElements)
        {
            this.GameNumber = gameNumber;
            this.ShowDebugElements = showDebugElements;
            foreach (Game item in games)
            {
                if (item.GameNumber == gameNumber)
                {
                    this.CurrentGame = item;
                    break;
                }
            }
        }

        public int GameNumber { get; private set; }
        public Game CurrentGame { get; private set; }
        public bool ShowDebugElements { get; private set; }

    }
}
