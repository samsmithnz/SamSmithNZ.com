using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.FooFighters.Service.Controllers
{
    public class ShowController : ApiController
    {
        public async Task<List<Show>> GetShowsByYear(int yearCode)
        {
            ShowDataAccess da = new ShowDataAccess();
            return await da.GetListByYearAsync(yearCode);
        }

        public async Task<List<Show>> GetShowsBySong(int songCode)
        {
            ShowDataAccess da = new ShowDataAccess();
            return await da.GetListBySongAsync(songCode);
        }

        public async Task<Show> GetShow(int showCode)
        {
            ShowDataAccess da = new ShowDataAccess();
            return await da.GetItemAsync(showCode);
        }

    }
}
