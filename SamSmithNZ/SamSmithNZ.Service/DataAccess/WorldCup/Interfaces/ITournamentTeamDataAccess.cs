using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface ITournamentTeamDataAccess 
    {
        Task<List<TournamentTeam>> GetQualifiedTeams(int tournamentCode);
        Task<List<TournamentTeam>> GetTeamsPlacingAsync(int tournamentCode);
        Task<bool> SaveItem(TournamentTeam tournamentTeam);
        Task<bool> DeleteItem(TournamentTeam tournamentTeam);

    }
}