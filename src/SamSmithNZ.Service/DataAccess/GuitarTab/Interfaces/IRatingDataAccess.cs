using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces
{
    public interface IRatingDataAccess
    {
        Task<List<Rating>> GetList();

    }
}


