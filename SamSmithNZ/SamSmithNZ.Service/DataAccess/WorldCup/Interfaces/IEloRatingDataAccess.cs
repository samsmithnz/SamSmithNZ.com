using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IEloRatingDataAccess
    {
        Task<List<TeamELORating>> UpdateTournamentELORatings(int tournamentCode);
        Task<List<TeamELORating>> CalculateEloForTournamentAsync(int tournamentCode);
        Task<bool> SaveTeamELORatingAsync(int tournamentCode, int teamCode, int eloRating);

    }
}
