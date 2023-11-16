namespace SamSmithNZ.Service.Models.WorldCup
{
    public class Playoff
    {
        public Playoff() { }

        public int TournamentCode { get; set; }
        public string RoundCode { get; set; }
        public int GameNumber { get; set; }
        public string Team1Prereq { get; set; }
        public string Team2Prereq { get; set; }
        public int SortOrder { get; set; }

    }
}



