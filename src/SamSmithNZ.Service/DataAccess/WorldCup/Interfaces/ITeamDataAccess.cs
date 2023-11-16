using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface ITeamDataAccess 
    {
        Task<List<Team>> GetList();
        Task<Team> GetItem(int teamCode);

    }
}
