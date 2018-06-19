using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class TournamentTopGoalScorerDataAccess : GenericDataAccess<TournamentTopGoalScorer>
    {

        public async Task<List<TournamentTopGoalScorer>> GetListAsync(int tournamentCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetListAsync("FB_GetTournamentTopGoalScorers", parameters);
        }
    }
}