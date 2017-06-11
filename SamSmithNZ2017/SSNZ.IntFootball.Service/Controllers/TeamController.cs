using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class TeamController : ApiController
    {
        public async Task<List<Team>> GetTeams()
        {
            TeamDataAccess da = new TeamDataAccess();
            return await da.GetListAsync();
        }
        public async Task<Team> GetTeam(int teamCode)
        {
            TeamDataAccess da = new TeamDataAccess();
            return await da.GetItemAsync(teamCode);
        }
    }
}
