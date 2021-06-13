using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGoalDataAccess
    {
        Task<List<Goal>> GetListByGame(int gameCode);
        Task<bool> SaveItem(Goal goal);
        Task<bool> DeleteItem(Goal goal);

    }
}