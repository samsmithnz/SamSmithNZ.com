using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces
{
    public interface IArtistDataAccess
    {
        Task<List<Artist>> GetList(bool? includeAllItems, bool isAdmin);

    }
}


