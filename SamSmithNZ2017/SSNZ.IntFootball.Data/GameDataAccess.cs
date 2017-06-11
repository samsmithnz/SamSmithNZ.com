using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GameDataAccess : GenericDataAccess<Game>
    {
        public async Task<List<Game>> GetListAsync(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);
            parameters.Add("@RoundCode", roundCode, DbType.String);

            return await base.GetListAsync("FB_GetGames", parameters);
        }

        public async Task<List<Game>> GetListAsyncByTeam(int teamCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            return await base.GetListAsync("FB_GetGames", parameters);
        }

        public async Task<List<Game>> GetListAsyncByPlayoff(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@RoundNumber", roundNumber, DbType.Int32);

            return await base.GetListAsync("FB_GetGames", parameters);
        }

    }
}