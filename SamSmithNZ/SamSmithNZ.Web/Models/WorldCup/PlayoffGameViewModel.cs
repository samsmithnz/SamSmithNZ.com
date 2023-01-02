using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class PlayoffGameViewModel
    {
        public PlayoffGameViewModel(List<Game> games, List<Playoff> playoffs, int gameNumber, bool showDebugElements, bool returnTeam1, string extraDetailsName, string scoreDetailsName)
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

            Playoff playoff = GetPlayoff(gameNumber, playoffs);
            string alternativeText = "";
            if (playoff != null)
            {
                if (returnTeam1)
                {
                    alternativeText = playoff.Team1Prereq;
                }
                else
                {
                    alternativeText = playoff.Team2Prereq;
                }
            }
            if (this.CurrentGame != null && this.CurrentGame.Team1Code > 0 && this.CurrentGame.Team2Code > 0)
            {
                GameResult result = GameViewModel.GetGameResult(this.CurrentGame);
                if (returnTeam1)
                {
                    this.TeamExtraText = result.Team1ExtraText;
                    this.TeamScoreText = result.Team1ScoreText;
                    this.Team1Score = result.Team1Score;
                    this.Team2Score = result.Team2Score;
                }
                else
                {
                    this.TeamExtraText = result.Team2ExtraText;
                    this.TeamScoreText = result.Team2ScoreText;
                    this.Team1Score = result.Team1Score;
                    this.Team2Score = result.Team2Score;
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

        private static Playoff GetPlayoff(int gameNumber, List<Playoff> playoffs)
        {
            Playoff result = null;
            foreach (Playoff playoff in playoffs)
            {
                if (playoff.GameNumber == gameNumber)
                {
                    result = playoff;
                    break;
                }
            }
            return result;
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
