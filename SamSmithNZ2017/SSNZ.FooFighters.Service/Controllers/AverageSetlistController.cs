using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.FooFighters.Service.Controllers
{
    public class AverageSetlistController : ApiController
    {
        public async Task<List<AverageSetlist>> GetAverageSetlist(int yearCode, int minimumSongCount = 0, bool showAllSongs = false)
        {
            AverageSetlistDataAccess da = new AverageSetlistDataAccess();
            return await da.GetListAsync(yearCode, minimumSongCount, showAllSongs);
        }

    }
}
