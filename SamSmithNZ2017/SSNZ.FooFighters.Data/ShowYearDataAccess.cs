using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace SSNZ.FooFighters.Data
{
    public class ShowYearDataAccess : GenericDataAccess<Year>
    {
        public async Task<List<Year>> GetListAsync()
        {
            return await base.GetListAsync("FFL_GetYearList");
        }
      
    }
}


