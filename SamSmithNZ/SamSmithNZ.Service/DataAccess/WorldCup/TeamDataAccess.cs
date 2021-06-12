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
    public class TeamDataAccess : BaseDataAccess<Team>, ITeamDataAccess
    {
        public TeamDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Team>> GetList()
        {
            DynamicParameters parameters = new();

            return await base.GetList("FB_GetTeams", parameters);
        }

        public async Task<Team> GetItem(int teamCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            return await base.GetItem("FB_GetTeams", parameters);
        }

    }
}
