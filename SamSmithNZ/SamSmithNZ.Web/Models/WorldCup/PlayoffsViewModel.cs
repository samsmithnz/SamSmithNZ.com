using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffsViewModel
    {
        public List<Playoff> Playoffs { get; set; }

        private List<Game> games;

        public List<Game> Games
        {
            get
            {
                return games;
            }
            set
            {
                games = value;

                //Find and mark rows that need the show penalties label
                bool firstRow3 = true;
                int gameCount = 0;
                foreach (Game game in games)
                {
                    if (game.RowType == 1)
                    {
                        gameCount++;
                    }
                    else if (game.RowType == 3 && firstRow3 == true)
                    {
                        //Mark the first row of PK's with a tag so we can use a label in the UI
                        firstRow3 = false;
                        if (game.Team1PenaltiesScore != null || game.Team2PenaltiesScore != null)
                        {
                            game.ShowPenaltyShootOutLabel = true;
                        }
                    }
                    else if (game.RowType != 3 && firstRow3 == false)
                    {
                        firstRow3 = true;
                    }
                }

                //Calaculate what sections to show in the playoffs graph
                Show16s = true;
                ShowQuarters = true;
                ShowSemis = true;
                Show3rdPlace = true;
                ShowFinals = true;
                switch (gameCount)
                {
                    case 16:
                        //Show Everything!
                        break;
                    case 15:
                        //Show 16 team playoff, no 3rd place
                        Show3rdPlace = false;
                        break;
                    case 8:
                        //Only show Quarter Finals
                        Show16s = false;
                        break;
                    case 7:
                        //Show Quarter Finals, hide 3rd place
                        Show16s = false;
                        Show3rdPlace = false;
                        break;
                    case 5:
                    case 4:
                        //Only show Semis
                        Show16s = false;
                        ShowQuarters = false;
                        break;
                    case 3:
                        //Show Semis and Finals, hide 3rd Place
                        Show16s = false;
                        ShowQuarters = false;
                        Show3rdPlace = false;
                        break;
                    case 2:
                        //Show Finals & 3rd place
                        Show16s = false;
                        ShowQuarters = false;
                        ShowSemis = false;
                        break;
                    case 1:
                        //Only show Finals
                        Show16s = false;
                        ShowQuarters = false;
                        ShowSemis = false;
                        Show3rdPlace = false;
                        break;
                };
            }
        }

        public bool Show16s { get; set; }
        public bool ShowQuarters { get; set; }
        public bool ShowSemis { get; set; }
        public bool Show3rdPlace { get; set; }
        public bool ShowFinals { get; set; }

        public string GetGameRowStyle(int gameCode)
        {
            string trStyle = "white";
            if ((gameCode % 2) == 1)
            {
                trStyle = "#f9f9f9";
            }
            return trStyle;
        }

    }
}
