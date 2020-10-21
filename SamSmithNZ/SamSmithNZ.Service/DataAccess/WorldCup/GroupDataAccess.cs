using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class GroupDataAccess : BaseDataAccess<Group>, IGroupDataAccess
    {
        public GroupDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Group>> GetList(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@RoundCode", roundCode, DbType.String);

            return await base.GetList("FB_GetGroups", parameters);
        }   
        
        public async Task<bool> DeleteItemAsync(Group group)
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.SaveItem("FB_DeleteGroup", parameters);
        }

    }
}
