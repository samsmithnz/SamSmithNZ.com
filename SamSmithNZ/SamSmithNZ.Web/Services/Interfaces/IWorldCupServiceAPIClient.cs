using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Services.Interfaces
{
    public interface IWorldCupServiceAPIClient
    {
        Task<bool> RefreshTournamentELORatings(int tournamentCode);
        Task<List<Game>> GetGames(int tournamentCode, int roundNumber, string roundCode, bool includeGoals);
        Task<List<Game>> GetGamesByTeam(int teamCode);
        Task<List<Game>> GetPlayoffGames(int tournamentCode, int roundNumber, bool includeGoals);
        Task<Game> GetGame(int gameCode);
        Task<List<GoalInsight>> GetGoalInsights(bool analyzeExtraTime);
        Task<List<GroupCode>> GetGroupCodes(int tournamentCode, int roundNumber);
        Task<List<Group>> GetGroups(int tournamentCode, int roundNumber, string roundCode);
        Task<List<Team>> GetTeams();
        Task<Team> GetTeam(int teamCode);
        Task<List<Tournament>> GetTournaments(int competitionCode = 1);
        Task<Tournament> GetTournament(int tournamentCode);
        Task<List<TournamentTeam>> GetTournamentQualifyingTeams(int tournamentCode);
        Task<List<TournamentTeam>> GetTournamentPlacingTeams(int tournamentCode);
        Task<List<TournamentTopGoalScorer>> GetTournamentTopGoalScorers(int tournamentCode, bool getOwnGoals);
    }
}
