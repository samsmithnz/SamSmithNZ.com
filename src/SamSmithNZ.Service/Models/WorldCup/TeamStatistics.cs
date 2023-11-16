using System.Collections.Generic;
using System.Text;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class TeamStatistics
    {
        public TeamStatistics()
        {
            _games = new();
        }

        public Team Team { get; set; }

        private List<Game> _games;

        public List<Game> Games
        {
            get
            {
                return _games;
            }
            set
            {
                int i = 0;
                StringBuilder sb = new();
                _games = value;
                if (_games != null)
                {
                    foreach (Game game in _games)
                    {
                        if (Team.TeamCode == game.Team1Code)
                        {
                            this.GamesExpectedWon += game.Team1OddsCountExpectedWin;
                            this.GamesExpectedLoss += game.Team1OddsCountExpectedLoss;
                            this.GamesUnexpectedWin += game.Team1OddsCountUnexpectedWin;
                            this.GamesUnexpectedLoss += game.Team1OddsCountUnexpectedLoss;
                            this.GamesUnexpectedDraw += game.Team1OddsCountUnexpectedDraw;
                            this.GamesUnknown += game.Team1OddsCountUnknown;
                        }
                        else if (Team.TeamCode == game.Team2Code)
                        {
                            this.GamesExpectedWon += game.Team2OddsCountExpectedWin;
                            this.GamesExpectedLoss += game.Team2OddsCountExpectedLoss;
                            this.GamesUnexpectedWin += game.Team2OddsCountUnexpectedWin;
                            this.GamesUnexpectedLoss += game.Team2OddsCountUnexpectedLoss;
                            this.GamesUnexpectedDraw += game.Team2OddsCountUnexpectedDraw;
                            this.GamesUnknown += game.Team2OddsCountUnknown;
                        }
                        //build record for last 5 games, filtering out games that haven't started
                        if (i < 5 && game.Team1NormalTimeScore != null && game.Team2NormalTimeScore != null)
                        {
                            if (game.Team1Code == Team.TeamCode)
                            {
                                if (game.Team1OddsCountExpectedWin > 0 || game.Team1OddsCountUnexpectedWin > 0)
                                {
                                    i++;
                                    sb.Append('W');
                                }
                                else if (game.Team1OddsCountUnexpectedDraw > 0)
                                {
                                    i++;
                                    sb.Append('D');
                                }
                                else if (game.Team1OddsCountExpectedLoss > 0 || game.Team1OddsCountUnexpectedLoss > 0)
                                {
                                    i++;
                                    sb.Append('L');
                                }
                            }
                            else
                            {
                                if (game.Team2OddsCountExpectedWin > 0 || game.Team2OddsCountUnexpectedWin > 0)
                                {
                                    i++;
                                    sb.Append('W');
                                }
                                else if (game.Team2OddsCountUnexpectedDraw > 0)
                                {
                                    i++;
                                    sb.Append('D');
                                }
                                else if (game.Team2OddsCountExpectedLoss > 0 || game.Team2OddsCountUnexpectedLoss > 0)
                                {
                                    i++;
                                    sb.Append('L');
                                }
                            }
                        }
                    }
                }
                this.TeamRecord = sb.ToString();

                this.GamesTotal = GamesExpectedWon + GamesExpectedLoss + GamesUnexpectedWin + GamesUnexpectedLoss + GamesUnexpectedDraw;
                this.ExpectedResultTotal = GamesExpectedWon + GamesExpectedLoss;
                if (GamesTotal > 0)
                {
                    this.ExpectedResultPercent = (double)ExpectedResultTotal / (double)GamesTotal;
                }
                this.UnexpectedResultTotal = GamesUnexpectedWin + GamesUnexpectedLoss + GamesUnexpectedDraw;
                if (GamesTotal > 0)
                {
                    this.UnexpectedResultPercent = (double)UnexpectedResultTotal / (double)GamesTotal;
                }

                this.GamesWonTotal = GamesExpectedWon + GamesUnexpectedWin;
                this.GamesLostTotal = GamesExpectedLoss + GamesUnexpectedLoss;

                if (GamesTotal > 0)
                {
                    this.GamesExpectedWinPercent = (double)GamesExpectedWon / (double)GamesTotal;
                    this.GamesExpectedLossPercent = (double)GamesExpectedLoss / (double)GamesTotal;
                    this.GamesUnexpectedWinPercent = (double)GamesUnexpectedWin / (double)GamesTotal;
                    this.GamesUnexpectedLossPercent = (double)GamesUnexpectedLoss / (double)GamesTotal;
                    this.GamesUnexpectedDrawPercent = (double)GamesUnexpectedDraw / (double)GamesTotal;
                }
            }
        }
        public string TeamRecord { get; set; }
        public int GamesExpectedWon { get; set; }
        public int GamesExpectedLoss { get; set; }
        public int GamesUnexpectedWin { get; set; }
        public int GamesUnexpectedLoss { get; set; }
        public int GamesUnexpectedDraw { get; set; }
        public int GamesUnknown { get; set; }


        public int GamesTotal { get; set; }
        public int ExpectedResultTotal { get; set; }
        public double ExpectedResultPercent { get; set; }
        public int UnexpectedResultTotal { get; set; }
        public double UnexpectedResultPercent { get; set; }

        public int GamesWonTotal { get; set; }
        public int GamesLostTotal { get; set; }

        public double GamesExpectedWinPercent { get; set; }
        public double GamesExpectedLossPercent { get; set; }
        public double GamesUnexpectedWinPercent { get; set; }
        public double GamesUnexpectedLossPercent { get; set; }
        public double GamesUnexpectedDrawPercent { get; set; }


    }
}
