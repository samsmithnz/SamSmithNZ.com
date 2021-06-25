using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IPlayoffSetupDataAccess
    {
        Task<List<PlayoffSetup>> GetList(int tournamentCode);
        Task<bool> SaveItem(PlayoffSetup setup);
    }
}