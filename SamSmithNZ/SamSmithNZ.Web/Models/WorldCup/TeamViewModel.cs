using Microsoft.AspNetCore.Mvc.Rendering;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Security.Permissions;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class TeamViewModel
    {
        public TeamViewModel(Team team, List<Game> games)
        {
            Team = team;
            Games = games;
            foreach (Game item in Games)
            {
                if (team.TeamCode == item.Team1Code)
                {
                    GamesExpectedWon += item.Team1OddsCountExpectedWin;
                    GamesExpectedLoss += item.Team1OddsCountExpectedLoss;
                    GamesUnexpectedWin += item.Team1OddsCountUnexpectedWin;
                    GamesUnexpectedLoss += item.Team1OddsCountUnexpectedLoss;
                    GamesUnexpectedDraw += item.Team1OddsCountUnexpectedDraw;
                    GamesUnknown += item.Team1OddsCountUnknown;
                }
                else if (team.TeamCode == item.Team2Code)
                {
                    GamesExpectedWon += item.Team2OddsCountExpectedWin;
                    GamesExpectedLoss += item.Team2OddsCountExpectedLoss;
                    GamesUnexpectedWin += item.Team2OddsCountUnexpectedWin;
                    GamesUnexpectedLoss += item.Team2OddsCountUnexpectedLoss;
                    GamesUnexpectedDraw += item.Team2OddsCountUnexpectedDraw;
                    GamesUnknown += item.Team2OddsCountUnknown;
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
            var trStyle = "";
            var green = "#CCFF99";
            var red = "#FFCCCC";
            var yellow = "lightgoldenrodyellow";

            var currentTeamResultWonGame = false;
            var currentTeamChanceToWin = 0;

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

