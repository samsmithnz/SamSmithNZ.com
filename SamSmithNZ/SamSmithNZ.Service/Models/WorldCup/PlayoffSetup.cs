namespace SamSmithNZ.Service.Models.WorldCup
{
    public class PlayoffSetup
    {
        public PlayoffSetup() { }

        public int TournamentCode { get; set; }
        public int RoundNumber { get; set; }
        public string RoundCode { get; set; }
        public int GameNumber { get; set; }
        public string Team1Prereq { get; set; }
        public string Team2Prereq { get; set; }

    }
}



