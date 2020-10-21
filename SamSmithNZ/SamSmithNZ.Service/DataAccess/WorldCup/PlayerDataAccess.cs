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
    public class PlayerDataAccess : BaseDataAccess<Player>, IPlayerDataAccess
    {
        public PlayerDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Player>> GetList(int gameCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetList("FB_GetPlayers", parameters);
        }

    }
}