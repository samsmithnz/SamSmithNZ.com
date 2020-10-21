using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGoalDataAccess
    {
        Task<List<Goal>> GetList(int gameCode);
        Task<bool> SaveItem(Goal goal);
        Task<bool> DeleteItemAsync(Goal goal);

    }
}