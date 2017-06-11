using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class TeamDataAccess : GenericDataAccess<Team>
    {

        public async Task<List<Team>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.GetListAsync("FB_GetTeams", parameters);
        }

        public async Task<Team> GetItemAsync(int teamCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            return await base.GetItemAsync("FB_GetTeams", parameters);
        }

    }
}
