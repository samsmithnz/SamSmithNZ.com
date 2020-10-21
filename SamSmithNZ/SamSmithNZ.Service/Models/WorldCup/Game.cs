using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public int Team1Code { get; set; }
        public string Team1Name { get; set; }
        public int? Team1NormalTimeScore { get; set; }
        public int? Team1ExtraTimeScore { get; set; }
        public int? Team1PenaltiesScore { get; set; }
        public int? Team1PreGameEloRating { get; set; }
        public int? Team1PostGameEloRating { get; set; }
        public int Team2Code { get; set; }
        public string Team2Name { get; set; }
        public int? Team2NormalTimeScore { get; set; }
        public int? Team2ExtraTimeScore { get; set; }
        public int? Team2PenaltiesScore { get; set; }
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

        public int? Team1TotalGoals
        {
            get
            {
                int? total = null;
                if (Team1NormalTimeScore != null)
                {
                    total = Team1NormalTimeScore;
                }
                if (Team1ExtraTimeScore != null)
                {
                    total += Team1ExtraTimeScore;
                }
                return total;
            }
        }
        public int? Team2TotalGoals
        {
            get
            {
                int? total = null;
                if (Team2NormalTimeScore != null)
                {
                    total = Team2NormalTimeScore;
                }
                if (Team2ExtraTimeScore != null)
                {
                    total += Team2ExtraTimeScore;
                }
                return total;
            }
        }

        //team_a_win_prob = 1.0/(10.0^((team_b - team_a)/400.0) + 1.0)

        public double Team1ChanceToWin
        {
            get
            {
                if (Team1PreGameEloRating == null || Team1Code == 0)
                {
                    return -1;
                }
                else
                {
                    double result = 1.0 / (Math.Pow(10, (((double)Team2PreGameEloRating - (double)Team1PreGameEloRating) / 400.0)) + 1.0) * 100;
                    //System.Diagnostics.Debug.WriteLine("Calculating Team1ChanceToWin:" + result.ToString());
                    //return 1.0 / (10.0 ^ ((Team2PreGameEloRating - Team1PreGameEloRating) / 400.0) + 1.0);
                    //return  1.0 / (Math.Pow(10, (((double)Team2PreGameEloRating - (double)Team1PreGameEloRating) / 400.0)) + 1.0);
                    return result;
                }
            }
        }
        public double Team2ChanceToWin
        {
            get
            {
                if (Team2PreGameEloRating == null || Team2Code == 0)
                {
                    return -1;
                }
                else
                {
                    double result = 1.0 / (Math.Pow(10, (((double)Team1PreGameEloRating - (double)Team2PreGameEloRating) / 400.0)) + 1.0) * 100;
                    //System.Diagnostics.Debug.WriteLine("Calculating Team2ChanceToWin:" + result.ToString());
                    //return 1.0 / (10.0 ^ ((Team2PreGameEloRating - Team1PreGameEloRating) / 400.0) + 1.0);
                    return result;
                }
            }
        }

        public bool Team1BeatOdds
        {
            get
            {
                if (Team1ChanceToWin >= 50)
                {
                    if (Team1ResultWonGame == true)
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
                    if (Team1ResultWonGame == true)
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

        public int Team1OddsCountExpectedWin
        {
            get
            {
                if (Team1OddsResultExpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountExpectedLoss
        {
            get
            {
                if (Team1OddsResultExpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedWin
        {
            get
            {
                if (Team1OddsResultUnexpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedLoss
        {
            get
            {
                if (Team1OddsResultUnexpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnexpectedDraw
        {
            get
            {
                if (Team1OddsResultUnexpectedDraw == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team1OddsCountUnknown
        {
            get
            {
                if (Team1OddsResultUnknown == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountExpectedWin
        {
            get
            {
                if (Team2OddsResultExpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountExpectedLoss
        {
            get
            {
                if (Team2OddsResultExpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedWin
        {
            get
            {
                if (Team2OddsResultUnexpectedWin == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedLoss
        {
            get
            {
                if (Team2OddsResultUnexpectedLoss == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnexpectedDraw
        {
            get
            {
                if (Team2OddsResultUnexpectedDraw == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
        public int Team2OddsCountUnknown
        {
            get
            {
                if (Team2OddsResultUnknown == true)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public bool Team1OddsResultUnknown
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Team1OddsResultExpectedWin
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ChanceToWin >= 50 && Team1ResultWonGame == true)
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

        public bool Team1OddsResultExpectedLoss
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ChanceToWin < 50 && Team1ResultWonGame == false && Team2ResultWonGame == true)
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

        public bool Team1OddsResultUnexpectedWin
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ChanceToWin < 50 && Team1ResultWonGame == true)
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

        public bool Team1OddsResultUnexpectedLoss
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ChanceToWin >= 50 && Team1ResultWonGame == false && Team2ResultWonGame == true)
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

        public bool Team1OddsResultUnexpectedDraw
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ResultWonGame == false && Team2ResultWonGame == false)
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

        public string Team1OddsStatusText
        {
            get
            {
                if (Team1ChanceToWin == -1)
                {
                    return "";
                }
                else if (Team1OddsResultExpectedWin == true)
                {
                    return Team1Name + " win: expected";
                }
                else if (Team1OddsResultUnexpectedLoss == true)
                {
                    return Team1Name + " loss: upset (win expected)";
                }
                else if (Team1OddsResultUnexpectedWin == true)
                {
                    return Team1Name + " win: upset (loss expected)";
                }
                else if (Team1OddsResultExpectedLoss == true)
                {
                    return Team1Name + " loss: expected";
                }
                else if (Team1OddsResultUnexpectedDraw == true && Team1ChanceToWin >= 50)
                {
                    return Team1Name + " draw: upset (win expected)";
                }
                else if (Team1OddsResultUnexpectedDraw == true && Team1ChanceToWin < 50)
                {
                    return Team1Name + " draw: upset (loss expected)";
                }

                return "";
            }
        }

        public bool Team2OddsResultUnknown
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool Team2OddsResultExpectedWin
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team2ChanceToWin >= 50 && Team2ResultWonGame == true)
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

        public bool Team2OddsResultExpectedLoss
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team2ChanceToWin < 50 && Team2ResultWonGame == false && Team1ResultWonGame == true)
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

        public bool Team2OddsResultUnexpectedWin
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team2ChanceToWin < 50 && Team2ResultWonGame == true)
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

        public bool Team2OddsResultUnexpectedLoss
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team2ChanceToWin >= 50 && Team2ResultWonGame == false && Team1ResultWonGame == true)
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

        public bool Team2OddsResultUnexpectedDraw
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return false;
                }
                else
                {
                    if (Team1ResultWonGame == false && Team2ResultWonGame == false)
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

        public string Team2OddsStatusText
        {
            get
            {
                if (Team2ChanceToWin == -1)
                {
                    return "";
                }
                else if (Team2OddsResultExpectedWin == true)
                {
                    return Team2Name + " win: expected";
                }
                else if (Team2OddsResultUnexpectedLoss == true)
                {
                    return Team2Name + " loss: upset (win expected)";
                }
                else if (Team2OddsResultUnexpectedWin == true)
                {
                    return Team2Name + " win: upset (loss expected)";
                }
                else if (Team2OddsResultExpectedLoss == true)
                {
                    return Team2Name + " loss: expected";
                }
                else if (Team2OddsResultUnexpectedDraw == true && Team2ChanceToWin >= 50)
                {
                    return Team2Name + " draw: upset (win expected)";
                }
                else if (Team2OddsResultUnexpectedDraw == true && Team2ChanceToWin < 50)
                {
                    return Team2Name + " draw: upset (loss expected)";
                }

                return "";
            }
        }

        public bool ShowPenaltyShootOutLabel { get; set; }

    }
}



