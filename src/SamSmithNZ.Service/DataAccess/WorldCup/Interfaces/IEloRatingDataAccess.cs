using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IEloRatingDataAccess
    {
        Task<bool> UpdateTournamentELORatings(int tournamentCode);
        Task<bool> UpdateGameELORating(int tournamentCode, int gameCode);
        Task<bool> SaveTeamELORatingAsync(int tournamentCode, int teamCode, int eloRating);
    }
}
