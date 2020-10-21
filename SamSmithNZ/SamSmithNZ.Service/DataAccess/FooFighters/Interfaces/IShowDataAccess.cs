using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters.Interfaces
{
    public interface IShowDataAccess 
    {

        Task<List<Show>> GetListByYearAsync(int yearCode);

        Task<List<Show>> GetListBySongAsync(int songCode);

        //Task<List<Show>> GetListByFFLCodeAsync();

        Task<Show> GetItem(int showCode);

        Task<bool> SaveItem(Show show);        
    }
}


