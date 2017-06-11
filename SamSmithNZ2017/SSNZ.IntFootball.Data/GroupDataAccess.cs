using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GroupDataAccess : GenericDataAccess<Group>
    {
        public async Task<List<Group>> GetListAsync(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@RoundCode", roundCode, DbType.String);

            return await base.GetListAsync("FB_GetGroups", parameters);
        }      

    }
}
