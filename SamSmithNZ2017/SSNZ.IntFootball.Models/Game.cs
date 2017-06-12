using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class Game
    {
        public Game() { }

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
        public int Team1NormalTimeScore { get; set; }
        public int? Team1ExtraTimeScore { get; set; }
        public int? Team1PenaltiesScore { get; set; }
        public int Team2Code { get; set; }
        public string Team2Name { get; set; }
        public int Team2NormalTimeScore { get; set; }
        public int? Team2ExtraTimeScore { get; set; }
        public int? Team2PenaltiesScore { get; set; }
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
        public bool? Team2ResultWonGame{ get; set; }
        //        ISNULL(te.fifa_ranking, 0) AS FifaRanking,
        //         NULL AS IsPenalty,
        //         NULL AS IsOwnGoal,
        //-1 AS SortOrder

    }
}
