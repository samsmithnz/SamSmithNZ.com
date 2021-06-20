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
    public class TournamentTopGoalScorerDataAccess : BaseDataAccess<TournamentTopGoalScorer>, ITournamentTopGoalScorerDataAccess
    {
        public TournamentTopGoalScorerDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<TournamentTopGoalScorer>> GetList(int tournamentCode, bool getOwnGoals)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@GetOwnGoals", getOwnGoals, DbType.Boolean);

            return await base.GetList("FB_GetTournamentTopGoalScorers", parameters);
        }
    }
}