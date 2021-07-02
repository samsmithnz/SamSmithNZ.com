using SamSmithNZ.Service.Models.WorldCup;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class TeamViewModel2
    {
        public TeamViewModel2(TeamStatistics teamStatistics)
        {
            this.TeamStatistics = teamStatistics;       
        }

        public TeamStatistics TeamStatistics { get; set; }       

        //Style the game rows to group game details with goal details
        public string GetIfFavoriteWonStyle(int team1Code, int currentTeamCode, double team1ChanceToWin, bool? team1ResultWonGame, bool? team2ResultWonGame)
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

