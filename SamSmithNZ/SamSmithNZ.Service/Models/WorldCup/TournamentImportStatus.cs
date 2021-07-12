namespace SamSmithNZ.Service.Models.WorldCup
{
    public class TournamentImportStatus
    {
        public TournamentImportStatus() { }

        public int CompetitionCode { get; set; }
        public int TournamentCode { get; set; }
        public int TournamentYear { get; set; }
        public int TotalGames { get; set; }
        public int TotalGamesCompleted { get; set; }
        public int TotalGoals { get; set; }
        public int TotalPenalties { get; set; }
        public int TotalShootoutGoals { get; set; }
        public decimal ImportingTotalPercentComplete { get; set; }
        public decimal ImportingTeamPercent { get; set; }
        public decimal ImportingGamePercent { get; set; }
        public decimal ImportingPlayerPercent { get; set; }
        public decimal ImportingGoalsPercent { get; set; }
        public decimal ImportingPenaltyShootoutGoalsPercent { get; set; }
    }
}
