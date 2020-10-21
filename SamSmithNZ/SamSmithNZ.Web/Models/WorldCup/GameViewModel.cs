using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class GameViewModel
    {
        public GameViewModel(List<Game> games, bool isTeamVersion, bool isGroupVersion, bool showDebugElements)
        {
            this.Games = games;
            this.IsTeamVersion = isTeamVersion;
            this.IsGroupVersion = isGroupVersion;
            this.ShowDebugElements = showDebugElements;
        }

        public List<Game> Games { get; private set; }
        public bool IsTeamVersion { get; private set; }
        public bool IsGroupVersion { get; private set; }
        public bool ShowDebugElements { get; private set; }

        public static GameResult GetGameResult(Game myGame)
        {
            GameResult gameResult = new GameResult();
            if (myGame.Team1PenaltiesScore >= 0 && myGame.Team2PenaltiesScore >=0)
            {
                gameResult.Team1Score = (int)(myGame.Team1NormalTimeScore + myGame.Team1ExtraTimeScore + myGame.Team1PenaltiesScore);
                gameResult.Team2Score = (int)(myGame.Team2NormalTimeScore + myGame.Team2ExtraTimeScore + myGame.Team2PenaltiesScore);
                gameResult.Team1ScoreText = ((int)(myGame.Team1NormalTimeScore + myGame.Team1ExtraTimeScore)).ToString() + " (" + myGame.Team1PenaltiesScore.ToString() + ")";
                gameResult.Team2ScoreText = ((int)(myGame.Team2NormalTimeScore + myGame.Team2ExtraTimeScore)).ToString() + " (" + myGame.Team2PenaltiesScore.ToString() + ")";
                if (gameResult.Team1Score > gameResult.Team2Score)
                {
                    gameResult.Team1ExtraText = " (pen)";
                    gameResult.Team2ExtraText = "";
                }
                if (gameResult.Team1Score < gameResult.Team2Score)
                {
                    gameResult.Team1ExtraText = "";
                    gameResult.Team2ExtraText = " (pen)";
                }
                gameResult.TeamScoresText = ((int)(myGame.Team1NormalTimeScore + myGame.Team1ExtraTimeScore)).ToString() + " - " + ((int)(myGame.Team2NormalTimeScore + myGame.Team2ExtraTimeScore)).ToString() + " (aet)";
                gameResult.TeamExtrasText = "(penalties)";
                gameResult.TeamPenaltiesText = "(" + myGame.Team1PenaltiesScore.ToString() + " - " + myGame.Team2PenaltiesScore.ToString() + ")";
            }
            else if (myGame.Team1ExtraTimeScore >= 0&& myGame.Team2ExtraTimeScore >= 0)
            {
                gameResult.Team1Score = (int)(myGame.Team1NormalTimeScore + myGame.Team1ExtraTimeScore);
                gameResult.Team2Score = (int)(myGame.Team2NormalTimeScore + myGame.Team2ExtraTimeScore);
                gameResult.Team1ScoreText = gameResult.Team1Score.ToString();
                gameResult.Team2ScoreText = gameResult.Team2Score.ToString();
                if (gameResult.Team1Score > gameResult.Team2Score)
                {
                    gameResult.Team1ExtraText = " (aet)";
                    gameResult.Team2ExtraText = "";
                }
                if (gameResult.Team1Score < gameResult.Team2Score)
                {
                    gameResult.Team1ExtraText = "";
                    gameResult.Team2ExtraText = " (aet)";
                }
                gameResult.TeamScoresText = ((int)(myGame.Team1NormalTimeScore + myGame.Team1ExtraTimeScore)).ToString() + " - " + ((int)(myGame.Team2NormalTimeScore + myGame.Team2ExtraTimeScore)).ToString() + " (aet)";
                gameResult.TeamExtrasText = "";
                gameResult.TeamPenaltiesText = "";
            }
            else
            {
                if (myGame.Team1NormalTimeScore >= 0 && myGame.Team2NormalTimeScore >= 0)
                {
                    gameResult.Team1Score = myGame.Team1NormalTimeScore;
                    gameResult.Team2Score = myGame.Team2NormalTimeScore;
                    gameResult.Team1ScoreText = gameResult.Team1Score.ToString();
                    gameResult.Team2ScoreText = gameResult.Team2Score.ToString();
                    gameResult.Team1ExtraText = "";
                    gameResult.Team2ExtraText = "";
                    gameResult.TeamScoresText = myGame.Team1NormalTimeScore.ToString() + " - " + myGame.Team2NormalTimeScore.ToString();
                    gameResult.TeamExtrasText = "";
                    gameResult.TeamPenaltiesText = "";
                }
            }

            return gameResult;
        }

    }

    public class GameResult
    {

        public int? Team1Score { get; set; }
        public int? Team2Score { get; set; }
        public int? Team1PenaltiesScore { get; set; }
        public int? Team2PenaltiesScore { get; set; }

        public string Team1ScoreText { get; set; }
        public string Team2ScoreText { get; set; }
        public string Team1ExtraText { get; set; }
        public string Team2ExtraText { get; set; }

        public string TeamScoresText { get; set; }
        public string TeamExtrasText { get; set; }
        public string TeamPenaltiesText { get; set; }

    }
}