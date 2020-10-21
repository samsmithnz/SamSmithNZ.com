using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGameGoalAssignmentDataAccess
    {
        Task<List<GameGoalAssignment>> GetList(int tournamentCode);

    }
}