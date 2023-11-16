using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces
{
    public interface ITabDataAccess
    {
        Task<List<Tab>> GetList(int albumCode, int sortOrder);
        Task<Tab> GetItem(int tabCode);
        Task<bool> SaveItem(Tab item);
        Task<bool> DeleteItem(int tabCode);

    }
}


