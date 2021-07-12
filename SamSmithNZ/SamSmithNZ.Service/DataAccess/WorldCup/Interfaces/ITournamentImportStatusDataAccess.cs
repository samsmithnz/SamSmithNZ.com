using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface ITournamentImportStatusDataAccess
    {
        Task<List<TournamentImportStatus>> GetList(int? competitionCode);

    }
}