using Dapper;
using SSNZ.IntFootball.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.IntFootball.Data
{
    public class GoalDataAccess : GenericDataAccess<Goal>
    {
        public async Task<List<Goal>> GetListAsync(int gameCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetListAsync("FB_GetGoals", parameters);
        }

        public async Task<bool> SaveItemAsync(Goal goal)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GoalCode", goal.GoalCode, DbType.Int32);
            parameters.Add("@GameCode", goal.GameCode, DbType.Int32);
            parameters.Add("@PlayerCode", goal.PlayerCode, DbType.Int32);
            parameters.Add("@GoalTime", goal.GoalTime, DbType.Int32);
            parameters.Add("@InjuryTime", goal.InjuryTime, DbType.Int32);
            parameters.Add("@IsPenalty", goal.IsPenalty, DbType.Boolean);
            parameters.Add("@IsOwnGoal", goal.IsOwnGoal, DbType.Boolean);

            return await base.PostItemAsync("FB_SaveGoal", parameters);
        }

    }
}