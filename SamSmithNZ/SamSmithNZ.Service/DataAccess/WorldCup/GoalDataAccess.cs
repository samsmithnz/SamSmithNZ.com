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
    public class GoalDataAccess : BaseDataAccess<Goal>, IGoalDataAccess
    {
        public GoalDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Goal>> GetListByGame(int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetList("FB_GetGoals", parameters);
        }

        public async Task<List<Goal>> GetList()
        {
            return await base.GetList("FB_GetGoals");
        }

        public async Task<bool> SaveItem(Goal goal)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GoalCode", goal.GoalCode, DbType.Int32);
            parameters.Add("@GameCode", goal.GameCode, DbType.Int32);
            parameters.Add("@PlayerCode", goal.PlayerCode, DbType.Int32);
            parameters.Add("@GoalTime", goal.GoalTime, DbType.Int32);
            parameters.Add("@InjuryTime", goal.InjuryTime, DbType.Int32);
            parameters.Add("@IsPenalty", goal.IsPenalty, DbType.Boolean);
            parameters.Add("@IsOwnGoal", goal.IsOwnGoal, DbType.Boolean);

            return await base.SaveItem("FB_SaveGoal", parameters);
        }

        public async Task<bool> DeleteItem(Goal goal)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GoalCode", goal.GoalCode, DbType.Int32);

            return await base.SaveItem("FB_DeleteGoal", parameters);

        }

    }
}