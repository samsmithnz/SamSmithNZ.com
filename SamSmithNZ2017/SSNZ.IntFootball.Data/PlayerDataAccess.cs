using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class PlayerDataAccess : GenericDataAccess<Player>
    {
        public async Task<List<Player>> GetListAsync(int gameCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetListAsync("FB_GetPlayers", parameters);
        }

    }
}