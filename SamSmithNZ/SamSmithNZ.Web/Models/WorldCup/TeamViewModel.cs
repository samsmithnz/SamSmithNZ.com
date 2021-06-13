using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class TeamViewModel
    {
        public TeamViewModel(Team team, List<Game> games)
        {
            this.Team = team;
            this.Games = games;
            foreach (Game item in this.Games)
            {
                if (team.TeamCode == item.Team1Code)
                {
                    this.GamesExpectedWon += item.Team1OddsCountExpectedWin;
                    this.GamesExpectedLoss += item.Team1OddsCountExpectedLoss;
                    this.GamesUnexpectedWin += item.Team1OddsCountUnexpectedWin;
                    this.GamesUnexpectedLoss += item.Team1OddsCountUnexpectedLoss;
                    this.GamesUnexpectedDraw += item.Team1OddsCountUnexpectedDraw;
                    this.GamesUnknown += item.Team1OddsCountUnknown;
                }
                else if (team.TeamCode == item.Team2Code)
                {
                    this.GamesExpectedWon += item.Team2OddsCountExpectedWin;
                    this.GamesExpectedLoss += item.Team2OddsCountExpectedLoss;
                    this.GamesUnexpectedWin += item.Team2OddsCountUnexpectedWin;
                    this.GamesUnexpectedLoss += item.Team2OddsCountUnexpectedLoss;
                    this.GamesUnexpectedDraw += item.Team2OddsCountUnexpectedDraw;
                    this.GamesUnknown += item.Team2OddsCountUnknown;
                }
            }
        }

        public Team Team { get; set; }
        public List<Game> Games { get; set; }
        public int GamesExpectedWon { get; set; }
        public int GamesExpectedLoss { get; set; }
        public int GamesUnexpectedWin { get; set; }
        public int GamesUnexpectedLoss { get; set; }
        public int GamesUnexpectedDraw { get; set; }
        public int GamesUnknown { get; set; }

        //Style the game rows to group game details with goal details
        public string GetDidFavoriteWinStyle(int team1Code, int currentTeamCode, double team1ChanceToWin, bool? team1ResultWonGame, bool? team2ResultWonGame)
        {
            string trStyle;
            string green = "#CCFF99";
            string red = "#FFCCCC";
            string yellow = "lightgoldenrodyellow";

            bool currentTeamResultWonGame = false;
            int currentTeamChanceToWin = 0;

            if (team1Code == currentTeamCode)
            {
                if (team1ResultWonGame == true)
                {
                    currentTeamResultWonGame = true;
                }
            }
            else
            {
                if (team2ResultWonGame == true)
                {
                    currentTeamResultWonGame = true;
                }
            }

            if (team1ChanceToWin < 0)
            {
                trStyle = "#FFFFFF";
            }
            else
            {
                if (team1ResultWonGame == false && team2ResultWonGame == false)
                {
                    trStyle = yellow;
                }
                else
                {
                    if (currentTeamChanceToWin >= 50)
                    {
                        if (currentTeamResultWonGame == true)
                        {
                            trStyle = green;
                        }
                        else
                        {
                            trStyle = red;
                        }
                    }
                    else
                    {
                        if (currentTeamResultWonGame == true)
                        {
                            trStyle = green;
                        }
                        else
                        {
                            trStyle = red;
                        }
                    }
                }
            }
            return trStyle;
        }
    }
}

