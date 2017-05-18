using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace SSNZ.FooFighters.Data
{
    public class ShowYearDataAccess : GenericDataAccess<ShowYear>
    {
        public async Task<List<ShowYear>> GetListAsync()
        {
            return await base.GetListAsync("FFL_GetYearList");
        }
      
    }
}


