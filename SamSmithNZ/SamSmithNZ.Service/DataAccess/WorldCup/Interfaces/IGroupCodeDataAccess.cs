using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGroupCodeDataAccess
    {
        Task<List<GroupCode>> GetList(int tournamentCode, int roundNumber);

    }
}
