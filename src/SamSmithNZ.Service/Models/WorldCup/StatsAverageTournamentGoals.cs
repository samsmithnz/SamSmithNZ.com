namespace SamSmithNZ.Service.Models.WorldCup
{
    public class StatsAverageTournamentGoals
    {
        public int TournamentCode { get; set; }
        public string TournamentName { get; set; }
        public decimal TotalGamesCompleted { get; set; }
        public decimal TotalGoals { get; set; }
        public decimal AverageGoalsPerGame { get; set; }
    }
}
