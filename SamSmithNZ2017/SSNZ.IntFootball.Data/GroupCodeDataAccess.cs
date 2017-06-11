using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GroupCodeDataAccess : GenericDataAccess<GroupCode>
    {

        public async Task<List<GroupCode>> GetListAsync(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);

            return await base.GetListAsync("FB_GetGroupCodes", parameters);
        }

    }
}
