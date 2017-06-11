using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class GroupCodeController : ApiController
    {
        public async Task<List<GroupCode>> GetGroupCodes(int tournamentCode, int roundNumber)
        {
            GroupCodeDataAccess da = new GroupCodeDataAccess();
            return await da.GetListAsync(tournamentCode, roundNumber);
        }
    }
}
