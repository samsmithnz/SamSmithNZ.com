using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffPart2ViewModel
    {
        public PlayoffPart2ViewModel(List<Game> games, int gameNumber, bool showDebugElements, bool returnTeam1, string extraDetailsName, string scoreDetailsName)
        {
            this.ShowDebugElements = showDebugElements;
            foreach (Game item in games)
            {
                if (item.GameNumber == gameNumber)
                {
                    this.CurrentGame = item;
                    break;
                }
            }

            //string teamScoreText = "";
            //string teamExtraText = "";
            //int team1Score = 0;
            //int team2Score = 0;
            string alternativeText = "";
            if (this.CurrentGame != null && (this.CurrentGame.Team1Code > 0 || this.CurrentGame.Team2Code > 0))
            {
                GameResult result = GameViewModel.GetGameResult(this.CurrentGame);
                if (returnTeam1 == true)
                {
                    this.TeamExtraText = result.Team1ExtraText;
                    this.TeamScoreText = result.Team1ScoreText;
                    this.Team1Score = result.Team1Score;
                    this.Team2Score = result.Team2Score;
                    //if (this.CurrentGame.Team1PenaltiesScore >= 0)
                    //{
                    //    teamScoreText = (this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore).ToString() + " (" + this.CurrentGame.Team1PenaltiesScore.ToString() + ")";
                    //    if (this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore + this.CurrentGame.Team1PenaltiesScore > this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore + this.CurrentGame.Team2PenaltiesScore)
                    //    {
                    //        teamExtraText = " (pen)";
                    //    }
                    //    team1Score = this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore + this.CurrentGame.Team1PenaltiesScore;
                    //    team2Score = this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore + this.CurrentGame.Team2PenaltiesScore;
                    //}
                    //else if (this.CurrentGame.Team1ExtraTimeScore >= 0)
                    //{
                    //    teamScoreText = (this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore).ToString();
                    //    if (this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore > this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore)
                    //    {
                    //        teamExtraText = " (aet)";
                    //    }
                    //    team1Score = this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore;
                    //    team2Score = this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore;
                    //}
                    //else
                    //{
                    //    if (this.CurrentGame.Team1NormalTimeScore >= 0)
                    //    {
                    //        teamScoreText = this.CurrentGame.Team1NormalTimeScore.ToString();
                    //        team1Score = this.CurrentGame.Team1NormalTimeScore;
                    //        team2Score = this.CurrentGame.Team2NormalTimeScore;
                    //    }
                    //}
                }
                else
                {
                    this.TeamExtraText = result.Team2ExtraText;
                    this.TeamScoreText = result.Team2ScoreText;
                    this.Team1Score = result.Team1Score;
                    this.Team2Score = result.Team2Score;
                    //if (this.CurrentGame.Team2PenaltiesScore >= 0)
                    //{
                    //    teamScoreText = (this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore).ToString() + " (" + this.CurrentGame.Team2PenaltiesScore.ToString() + ")";
                    //    if (this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore + this.CurrentGame.Team2PenaltiesScore > this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore + this.CurrentGame.Team2PenaltiesScore)
                    //    {
                    //        teamExtraText = " (pen)";
                    //    }
                    //    team1Score = this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore + this.CurrentGame.Team1PenaltiesScore;
                    //    team2Score = this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore + this.CurrentGame.Team2PenaltiesScore;
                    //}
                    //else if (this.CurrentGame.Team2ExtraTimeScore >= 0)
                    //{
                    //    teamScoreText = (this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore).ToString();
                    //    if (this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore > this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore)
                    //    {
                    //        teamExtraText = " (aet)";
                    //    }
                    //    team1Score = this.CurrentGame.Team1NormalTimeScore + this.CurrentGame.Team1ExtraTimeScore;
                    //    team2Score = this.CurrentGame.Team2NormalTimeScore + this.CurrentGame.Team2ExtraTimeScore;
                    //}
                    //else
                    //{
                    //    if (this.CurrentGame.Team2NormalTimeScore >= 0)
                    //    {
                    //        teamScoreText = this.CurrentGame.Team2NormalTimeScore.ToString();
                    //        team1Score = this.CurrentGame.Team1NormalTimeScore;
                    //        team2Score = this.CurrentGame.Team2NormalTimeScore;
                    //    }
                    //}
                }
            }
            else
            {
                switch (gameNumber)
                {
                    case 49:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group A";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group B";
                        }
                        break;
                    case 50:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group C";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group D";
                        }
                        break;
                    case 51:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group D";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group C";
                        }
                        break;
                    case 52:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group B";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group A";
                        }
                        break;
                    case 53:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group E";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group F";
                        }
                        break;
                    case 54:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group G";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group H";
                        }
                        break;
                    case 55:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group F";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group E";
                        }
                        break;
                    case 56:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Group H";
                        }
                        else
                        {
                            alternativeText = "Runner Up Group G";
                        }
                        break;
                    case 57:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 53";
                        }
                        else
                        {
                            alternativeText = "Winner Game 54";
                        }
                        break;
                    case 58:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 49";
                        }
                        else
                        {
                            alternativeText = "Winner Game 50";
                        }
                        break;
                    case 59:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 52";
                        }
                        else
                        {
                            alternativeText = "Winner Game 51";
                        }
                        break;
                    case 60:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 55";
                        }
                        else
                        {
                            alternativeText = "Winner Game 56";
                        }
                        break;
                    case 61:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 58";
                        }
                        else
                        {
                            alternativeText = "Winner Game 57";
                        }
                        break;
                    case 62:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 59";
                        }
                        else
                        {
                            alternativeText = "Winner Game 60";
                        }
                        break;
                    case 63:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Loser Game 61";
                        }
                        else
                        {
                            alternativeText = "Loser Game 62";
                        }
                        break;
                    case 64:
                        if (returnTeam1 == true)
                        {
                            alternativeText = "Winner Game 61";
                        }
                        else
                        {
                            alternativeText = "Winner Game 62";
                        }
                        break;
                }
            }
            this.ReturnTeam1 = returnTeam1;
            //this.TeamExtraText = teamExtraText;
            //this.TeamScoreText = teamScoreText;
            //this.Team1Score = team1Score;
            //this.Team2Score = team2Score;
            this.AlternativeText = alternativeText;
            this.ExtraDetailsTDName = extraDetailsName;
            this.ScoreDetailsTDName = scoreDetailsName;
        }

        public Game CurrentGame { get; private set; }
        public bool ShowDebugElements { get; private set; }
        public string TeamScoreText { get; private set; }
        public string TeamExtraText { get; private set; }
        public bool ReturnTeam1 { get; private set; }
        public int? Team1Score { get; private set; }
        public int? Team2Score { get; private set; }
        public string AlternativeText { get; private set; }
        public string ExtraDetailsTDName { get; private set; }
        public string ScoreDetailsTDName { get; private set; }

    }
}
