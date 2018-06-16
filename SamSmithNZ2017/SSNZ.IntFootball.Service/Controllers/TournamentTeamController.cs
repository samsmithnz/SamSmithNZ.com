using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.IntFootball.Service.Controllers
{
    public class TournamentTeamController : ApiController
    {
        public async Task<List<TournamentTeam>> GetTournamentQualifyingTeams(int tournamentCode)
        {
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            return await da.GetQualifiedTeamsAsync(tournamentCode);
        }
        public async Task<List<TournamentTeam>> GetTournamentPlacingTeams(int tournamentCode)
        {
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            return await da.GetTeamsPlacingAsync(tournamentCode);
        }

        public async Task<bool> RefreshTournamentELORatings(int tournamentCode)
        {
            EloRatingDataAccess daELO = new EloRatingDataAccess();
            List<TeamELORating> teams = await daELO.CalculateEloForTournamentAsync(tournamentCode);
            foreach (TeamELORating team in teams)
            {
                await daELO.SaveTeamELORatingAsync(team.TournamentCode, team.TeamCode, team.ELORating);               
            }
            return true;
        }
    }
}
