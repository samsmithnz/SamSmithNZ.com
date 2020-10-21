using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface ITournamentDataAccess 
    {
        Task<List<Tournament>> GetList(int? competitionCode);
        Task<Tournament> GetItem(int tournamentCode);

    }
}