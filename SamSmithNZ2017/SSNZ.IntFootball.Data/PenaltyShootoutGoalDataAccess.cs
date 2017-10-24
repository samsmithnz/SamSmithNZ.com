using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class PenaltyShootoutGoalDataAccess : GenericDataAccess<PenaltyShootoutGoal>
    {
        public async Task<List<PenaltyShootoutGoal>> GetListAsync(int gameCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetListAsync("FB_GetPenaltyShootoutGoals", parameters);
        }

        public async Task<bool> SaveItemAsync(PenaltyShootoutGoal goal)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PenaltyCode", goal.PenaltyCode, DbType.Int32);
            parameters.Add("@GameCode", goal.GameCode, DbType.Int32);
            parameters.Add("@PlayerCode", goal.PlayerCode, DbType.Int32);
            parameters.Add("@PenaltyOrder", goal.PenaltyOrder, DbType.Int32);
            parameters.Add("@Scored", goal.Scored, DbType.Boolean);

            return await base.PostItemAsync("FB_SavePenaltyShootoutGoal", parameters);
        }
    }
}