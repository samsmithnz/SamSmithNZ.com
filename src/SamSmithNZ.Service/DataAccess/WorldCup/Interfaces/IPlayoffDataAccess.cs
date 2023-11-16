using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IPlayoffDataAccess
    {
        Task<List<Playoff>> GetList(int tournamentCode);
        Task<bool> SaveItem(Playoff setup);
    }
}