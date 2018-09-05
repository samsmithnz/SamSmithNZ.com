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

        public async Task<bool> SaveItemAsync(TournamentTeam tournamentTeam)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentTeam.TournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", tournamentTeam.TeamCode, DbType.Int32);

            return await base.PostItemAsync("FB_SaveTournamentTeam", parameters);
        }

        public async Task<bool> DeleteItemAsync(TournamentTeam tournamentTeam)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentTeam.TournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", tournamentTeam.TeamCode, DbType.Int32);

            return await base.PostItemAsync("FB_DeleteTournamentTeam", parameters);
        }

    }
}