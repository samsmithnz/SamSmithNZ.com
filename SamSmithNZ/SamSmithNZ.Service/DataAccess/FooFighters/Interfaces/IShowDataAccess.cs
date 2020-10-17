using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public interface IShowDataAccess 
    {

        Task<List<Show>> GetListByYearAsync(int yearCode);

        Task<List<Show>> GetListBySongAsync(int songCode);

        //Task<List<Show>> GetListByFFLCodeAsync();

        Task<Show> GetItemAsync(int showCode);

        Task<bool> SaveItemAsync(Show show);        
    }
}


