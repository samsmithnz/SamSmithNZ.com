using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GameDataAccess : GenericDataAccess<Game>
    {
        public async Task<List<Game>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.GetListAsync("spIFB_GetGameList", parameters);
        }

        public async Task<List<Game>> GetListAsyncByTournament(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);

            return await base.GetListAsync("spIFB_GetGameList", parameters);
        }

        public async Task<List<Game>> GetListAsyncByTeam(int teamCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@team_code", teamCode, DbType.Int32);

            return await base.GetListAsync("spIFB_GetGameListForTeam", parameters);
        }

        public async Task<List<Game>> GetListAsync(int tournamentCode, int roundNumber, string roundCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);
            parameters.Add("@round_number", roundNumber, DbType.Int32);
            parameters.Add("@round_code", roundCode, DbType.String);

            return await base.GetListAsync("spIFB_GetGameList", parameters);
        }

        public async Task<List<Game>> GetListAsyncByPlayoff(int tournamentCode, int roundNumber)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);
            parameters.Add("@round_number", roundNumber, DbType.Int32);

            return await base.GetListAsync("spIFB_GetPlayoffGameList", parameters);
        }    
        
    }
}