using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IPenaltyShootoutGoalDataAccess
    {
        Task<List<PenaltyShootoutGoal>> GetList(int gameCode);
        Task<bool> SaveItem(PenaltyShootoutGoal goal);
        Task<bool> DeleteItemAsync(PenaltyShootoutGoal goal);

    }
}