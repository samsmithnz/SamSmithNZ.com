using System;

namespace SamSmithNZ.Service.Models.WorldCup
{
    public class Game
    {
        public Game() { }

        public int RowType { get; set; }
        public int RoundNumber { get; set; }
        public string RoundCode { get; set; }
        public string RoundName { get; set; }
        public string Location { get; set; }
        public int TournamentCode { get; set; }
        public string TournamentName { get; set; }

        public int GameCode { get; set; }
        public int GameNumber { get; set; }
        public DateTime GameTime { get; set; }

        public string GameTimeString()
        {
            if (GameTime.TimeOfDay == new TimeSpan(0, 0, 0))
            {
                return GameTime.ToString("dd-MMM-yyyy");
            }
            else
            {
                return GameTime.ToString("dd-MMM-yyyy HH:mm");
            }
        }
        public int Team1Code { get; set; }
        public string Team1Name { get; set; }
        public int? Team1NormalTimeScore { get; set; }
        public int? Team1ExtraTimeScore { get; set; }
        public int? Team1PenaltiesScore { get; set; }
        public int? Team1EloRating { get; set; }
        public int? Team1PreGameEloRating { get; set; }
        public int? Team1PostGameEloRating { get; set; }
        public int Team2Code { get; set; }
        public string Team2Name { get; set; }
        public int? Team2NormalTimeScore { get; set; }
        public int? Team2ExtraTimeScore { get; set; }
        public int? Team2PenaltiesScore { get; set; }
        public int? Team2EloRating { get; set; }
        public int? Team2PreGameEloRating { get; set; }
        public int? Team2PostGameEloRating { get; set; }
        public string Team1FlagName { get; set; }
        public string Team2FlagName { get; set; }
        public bool Team1Withdrew { get; set; }
        public bool Team2Withdrew { get; set; }
        public string CoachName { get; set; }
        public string CoachFlag { get; set; }

        //These fields are all derived from the above database columns
        public int? Team1ResultRegulationTimeScore { get; set; }
        public int? Team2ResultRegulationTimeScore { get; set; }
        public string Team1ResultInformation { get; set; }
        public string Team2ResultInformation { get; set; }
        public bool? Team1ResultWonGame { get; set; }
        public bool? Team2ResultWonGame { get; set; }

        //Player properties
        public bool IsPenalty { get; set; }
        public bool IsOwnGoal { get; set; }
        public bool IsGoldenGoal { get; set; }

        public int? Team1TotalGoals {
            get {
                int? total = null;
                if (this.Team1NormalTimeScore != null)
                {
                    total = this.Team1NormalTimeScore;
                }
                if (this.Team1ExtraTimeScore != null)
                {
                    total += this.Team1ExtraTimeScore;
                }
                return total;
            }
        }
        public int? Team2TotalGoals {
            get {
                int? total = null;
                if (this.Team2NormalTimeScore != null)
                {
                    total = this.Team2NormalTimeScore;
                }
                if (this.Team2ExtraTimeScore != null)
                {
                    total += this.Team2ExtraTimeScore;
                }
                return total;
            }
        }

        //team_a_win_prob = 1.0/(10.0^((team_b - team_a)/400.0) + 1.0)

        public double Team1ChanceToWin {
            get {
                if (this.Team1PreGameEloRating == null || this.Team1Code == 0)
                {
                    return -1;
                }
                else
                {
                    double result = 1.0 / (Math.Pow(10, (((double)this.Team2EloRating - (double)this.Team1EloRating) / 400.0)) + 1.0) * 100;
                    //System.Diagnostics.Debug.WriteLine("Calculating Team1ChanceToWin:" + result.ToString());
                    //return 1.0 / (10.0 ^ ((Team2PreGameEloRating - Team1PreGameEloRating) / 400.0) + 1.0);
                    //return  1.0 / (Math.Pow(10, (((double)Team2PreGameEloRating - (double)Team1PreGameEloRating) / 400.0)) + 1.0);
                    return result;
                }
            }
        }

        public double Team2ChanceToWin {
            get {
                if (this.Team2PreGameEloRating == null || this.Team2Code == 0)
                {
                    return -1;
                }
                else
                {
                    double result = 1.0 / (Math.Pow(10, (((double)this.Team1EloRating - (double)this.Team2EloRating) / 400.0)) + 1.0) * 100;
                    //System.Diagnostics.Debug.WriteLine("Calculating Team2ChanceToWin:" + result.ToString());
                    //return 1.0 / (10.0 ^ ((Team2PreGameEloRating - Team1PreGameEloRating) / 400.0) + 1.0);
                    return result;
                }
            }
        }

        public bool Team1BeatOdds {
            get {
                if (this.Team1ChanceToWin >= 50)
                {
                    if (this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                //<span ng-if="item.Team1Code == team.TeamCode && item.Team1ChanceToWin > 50 && item.Team1ResultWonGame==true" style="width:100%; height: 100%; background-color: #CCFF99;">{{team.TeamName}} won as fav</span>
                //<span ng-if="item.Team1Code == team.TeamCode && item.Team1ChanceToWin > 50 && item.Team1ResultWonGame==false" style="width:100%; height: 100%; background-color: #FFCCCC;">Opponent won as underdog</span>
                //<span ng-if="item.Team1Code == team.TeamCode && item.Team1ChanceToWin < 50 && item.Team1ResultWonGame==true" style="width:100%; height: 100%; background-color: #CCFF99;">{{team.TeamName}} won as underdog</span>
                //<span ng-if="item.Team1Code == team.TeamCode && item.Team1ChanceToWin < 50 && item.Team1ResultWonGame==false" style="width:100%; height: 100%; background-color: #FFCCCC;">Opponent won as fav</span>
                //<span ng-if="item.Team2Code == team.TeamCode && item.Team2ChanceToWin > 50 && item.Team2ResultWonGame==true" style="width:100%; height: 100%; background-color: #CCFF99;">{{team.TeamName}} won as fav</span>
                //<span ng-if="item.Team2Code == team.TeamCode && item.Team2ChanceToWin > 50 && item.Team2ResultWonGame==false" style="width:100%; height: 100%; background-color: #FFCCCC;">Opponent won as underdog</span>
                //<span ng-if="item.Team2Code == team.TeamCode && item.Team2ChanceToWin < 50 && item.Team2ResultWonGame==true" style="width:100%; height: 100%; background-color: #CCFF99;">{{team.TeamName}} won as underdog</span>
                //<span ng-if="item.Team2Code == team.TeamCode && item.Team2ChanceToWin < 50 && item.Team2ResultWonGame==false" style="width:100%; height: 100%; background-color: #FFCCCC;">Opponent won as fav</span>
                //<span ng-id="item.Team1ResultWonGame==false && item.Team2ResultWonGame==false" style="width:100%; height: 100%; background-color: lightgoldenrodyellow;">No result - draw</span>
            }
        }

        public int Team1OddsCountExpectedWin {
            get {
                if (this.Team1OddsResultExpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountExpectedLoss {
            get {
                if (this.Team1OddsResultExpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedWin {
            get {
                if (this.Team1OddsResultUnexpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedLoss {
            get {
                if (this.Team1OddsResultUnexpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedDraw {
            get {
                if (this.Team1OddsResultUnexpectedDraw == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnknown {
            get {
                if (this.Team1OddsResultUnknown == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountExpectedWin {
            get {
                if (this.Team2OddsResultExpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountExpectedLoss {
            get {
                if (this.Team2OddsResultExpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedWin {
            get {
                if (this.Team2OddsResultUnexpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedLoss {
            get {
                if (this.Team2OddsResultUnexpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedDraw {
            get {
                if (this.Team2OddsResultUnexpectedDraw == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnknown {
            get {
                if (this.Team2OddsResultUnknown == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool Team1OddsResultUnknown {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Team1OddsResultExpectedWin {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ChanceToWin >= 50 && this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team1OddsResultExpectedLoss {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ChanceToWin < 50 && this.Team1ResultWonGame == false && this.Team2ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team1OddsResultUnexpectedWin {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ChanceToWin < 50 && this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team1OddsResultUnexpectedLoss {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ChanceToWin >= 50 && this.Team1ResultWonGame == false && this.Team2ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team1OddsResultUnexpectedDraw {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ResultWonGame == false && this.Team2ResultWonGame == false && this.Team1NormalTimeScore != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public string Team1OddsStatusText {
            get {
                if (this.Team1ChanceToWin == -1)
                {
                    return "";
                }
                else if (this.Team1OddsResultExpectedWin == true)
                {
                    return this.Team1Name + " win: expected";
                }
                else if (this.Team1OddsResultUnexpectedLoss == true)
                {
                    return this.Team1Name + " loss: upset (win expected)";
                }
                else if (this.Team1OddsResultUnexpectedWin == true)
                {
                    return this.Team1Name + " win: upset (loss expected)";
                }
                else if (this.Team1OddsResultExpectedLoss == true)
                {
                    return this.Team1Name + " loss: expected";
                }
                else if (this.Team1OddsResultUnexpectedDraw == true && this.Team1ChanceToWin >= 50)
                {
                    return this.Team1Name + " draw: upset (win expected)";
                }
                else if (this.Team1OddsResultUnexpectedDraw == true && this.Team1ChanceToWin < 50)
                {
                    return this.Team1Name + " draw: upset (loss expected)";
                }

                return "";
            }
        }

        public bool Team2OddsResultUnknown {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Team2OddsResultExpectedWin {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team2ChanceToWin >= 50 && this.Team2ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team2OddsResultExpectedLoss {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team2ChanceToWin < 50 && this.Team2ResultWonGame == false && this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team2OddsResultUnexpectedWin {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team2ChanceToWin < 50 && this.Team2ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team2OddsResultUnexpectedLoss {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team2ChanceToWin >= 50 && this.Team2ResultWonGame == false && this.Team1ResultWonGame == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public bool Team2OddsResultUnexpectedDraw {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (this.Team1ResultWonGame == false && this.Team2ResultWonGame == false && this.Team2NormalTimeScore != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public string Team2OddsStatusText {
            get {
                if (this.Team2ChanceToWin == -1)
                {
                    return "";
                }
                else if (this.Team2OddsResultExpectedWin == true)
                {
                    return this.Team2Name + " win: expected";
                }
                else if (this.Team2OddsResultUnexpectedLoss == true)
                {
                    return this.Team2Name + " loss: upset (win expected)";
                }
                else if (this.Team2OddsResultUnexpectedWin == true)
                {
                    return this.Team2Name + " win: upset (loss expected)";
                }
                else if (this.Team2OddsResultExpectedLoss == true)
                {
                    return this.Team2Name + " loss: expected";
                }
                else if (this.Team2OddsResultUnexpectedDraw == true && this.Team2ChanceToWin >= 50)
                {
                    return this.Team2Name + " draw: upset (win expected)";
                }
                else if (this.Team2OddsResultUnexpectedDraw == true && this.Team2ChanceToWin < 50)
                {
                    return this.Team2Name + " draw: upset (loss expected)";
                }

                return "";
            }
        }

        public bool ShowPenaltyShootOutLabel { get; set; }

    }
}



