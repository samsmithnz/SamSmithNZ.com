using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.FooFighters.Service.Controllers
{
    public class YearController : ApiController
    {
        public async Task<List<Year>> GetYears()
        {
            ShowYearDataAccess da = new ShowYearDataAccess();
            return await da.GetListAsync();
        }

    }
}
