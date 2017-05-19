using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GroupDetailDataAccess : GenericDataAccess<GroupDetail>
    {
        public async Task<List<GroupDetail>> GetListAsync(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);
            parameters.Add("@round_number", roundNumber, DbType.Int32);
            parameters.Add("@round_code", roundCode, DbType.String);

            return await base.GetListAsync("spIFB_GetGroupDetails", parameters);
        }      

    }
}
