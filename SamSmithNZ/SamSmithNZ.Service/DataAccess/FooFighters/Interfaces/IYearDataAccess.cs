using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public interface IYearDataAccess 
    {
        Task<List<Year>> GetListAsync();
              
    }
}


