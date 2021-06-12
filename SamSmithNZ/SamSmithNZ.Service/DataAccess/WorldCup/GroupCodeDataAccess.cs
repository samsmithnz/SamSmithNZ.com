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
    public class GroupCodeDataAccess : BaseDataAccess<GroupCode>, IGroupCodeDataAccess
    {
        public GroupCodeDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }


        public async Task<List<GroupCode>> GetList(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);

            return await base.GetList("FB_GetGroupCodes", parameters);
        }

    }
}
