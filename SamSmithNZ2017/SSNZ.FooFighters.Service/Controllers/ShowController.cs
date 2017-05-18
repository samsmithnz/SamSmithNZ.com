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

        public async Task<List<Show>> GetShowsBySong(int songKey)
        {
            ShowDataAccess da = new ShowDataAccess();
            return await da.GetListBySongAsync(songKey);
        }

        public async Task<Show> GetShow(int showKey)
        {
            ShowDataAccess da = new ShowDataAccess();
            return await da.GetItemAsync(showKey);
        }

    }
}
