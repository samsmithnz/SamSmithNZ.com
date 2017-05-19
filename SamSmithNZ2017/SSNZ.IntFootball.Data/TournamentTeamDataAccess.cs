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
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);

            return await base.GetListAsync("spIFB_GetTournamentTeams", parameters);
        }
     
        public async Task<List<TournamentTeam>> GetTeamsPlacingAsync(int competitionCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", competitionCode, DbType.Int32);

            return await base.GetListAsync("spIFB_GetTournamentTeamsPlacing", parameters);
        }      

    }
}