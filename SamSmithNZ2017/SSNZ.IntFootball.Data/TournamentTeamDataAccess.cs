using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class TournamentTeamDataAccess : GenericDataAccess<TournamentTeam>
    {

        public async Task<List<TournamentTeam>> GetQualifiedTeamsAsync(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetListAsync("FB_GetTournamentTeams", parameters);
        }

        public async Task<List<TournamentTeam>> GetTeamsPlacingAsync(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetListAsync("FB_GetTournamentTeamsPlacing", parameters);
        }

    }
}