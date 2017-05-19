using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class TournamentDataAccess : GenericDataAccess<Tournament>
    {
        public async Task<List<Tournament>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.GetListAsync("spIFB_GetTournaments", parameters);
        }

        public async Task<List<Tournament>> GetListAsync(int competitionCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@competition_code", competitionCode, DbType.Int32);

            return await base.GetListAsync("spIFB_GetTournaments", parameters);
        }
       

        public async Task<Tournament> GetItemAsync(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@tournament_code", tournamentCode, DbType.Int32);

            return await base.GetItemAsync("spIFB_GetTournaments", parameters);
        }      

    }
}