using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class GroupController : ApiController
    {
        public async Task<List<Group>> GetGroups(int tournamentCode, int roundNumber, string roundCode)
        {
            GroupDataAccess da = new GroupDataAccess();
            return await da.GetListAsync(tournamentCode, roundNumber, roundCode);
        }
    }
}
