using SamSmithNZ.Service.Models.GuitarTab;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.GuitarTab.Interfaces
{
    public interface ISearchDataAccess
    {
        Task<List<Search>> GetList(Guid? recordid);
        Task<Guid> SaveItem(String searchText);

    }
}
