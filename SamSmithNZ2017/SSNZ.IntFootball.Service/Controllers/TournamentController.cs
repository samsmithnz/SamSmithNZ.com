using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class TournamentController : ApiController
    {
        public async Task<List<Tournament>> GetTournaments()
        {
            TournamentDataAccess da = new TournamentDataAccess();
            return await da.GetListAsync();
        }
        public async Task<Tournament> GetTournament(int tournamentCode)
        {
            TournamentDataAccess da = new TournamentDataAccess();
            return await da.GetItemAsync(tournamentCode);
        }
    }
}
