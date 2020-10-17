using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public class YearDataAccess : BaseDataAccess<Year>, IYearDataAccess
    {
        public YearDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Year>> GetListAsync()
        {
            return await base.GetList("FFL_GetYearList");
        }
      
    }
}


