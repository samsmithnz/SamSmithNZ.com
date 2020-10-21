using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGroupDataAccess
    {
        Task<List<Group>> GetList(int tournamentCode, int roundNumber, string roundCode);
        Task<bool> DeleteItemAsync(Group group);

    }
}
