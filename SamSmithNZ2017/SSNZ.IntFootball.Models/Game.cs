using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSNZ.IntFootball.Models
{
    public class Game
    {
        public Game() { }

        public short RoundNumber { get; set; }
        public string RoundName { get; set; }
        public short GameCode { get; set; }
        public short GameNumber { get; set; }
        public DateTime GameTime { get; set; }
        public short Team1Code { get; set; }
        public string Team1Name { get; set; }
        public short Team1NormalTimeScore { get; set; }
        public short Team1ExtraTimeScore { get; set; }
        public short Team1PenaltiesScore { get; set; }
        public short Team2Code { get; set; }
        public string Team2Name { get; set; }
        public short Team2NormalTimeScore { get; set; }
        public short Team2ExtraTimeScore { get; set; }
        public short Team2PenaltiesScore { get; set; }
        public string RoundCode { get; set; }
        public string Location { get; set; }
        public string Team1FlagName { get; set; }
        public string Team2FlagName { get; set; }
        public bool Team1Withdrew { get; set; }
        public bool Team2Withdrew { get; set; }

    }
}
