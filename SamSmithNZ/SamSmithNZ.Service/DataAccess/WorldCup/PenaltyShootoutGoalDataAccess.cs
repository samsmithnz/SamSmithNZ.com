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
    public class PenaltyShootoutGoalDataAccess : BaseDataAccess<PenaltyShootoutGoal>, IPenaltyShootoutGoalDataAccess
    {
        public PenaltyShootoutGoalDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<PenaltyShootoutGoal>> GetList(int gameCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetList("FB_GetPenaltyShootoutGoals", parameters);
        }

        public async Task<bool> SaveItem(PenaltyShootoutGoal goal)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PenaltyCode", goal.PenaltyCode, DbType.Int32);
            parameters.Add("@GameCode", goal.GameCode, DbType.Int32);
            parameters.Add("@PlayerCode", goal.PlayerCode, DbType.Int32);
            parameters.Add("@PenaltyOrder", goal.PenaltyOrder, DbType.Int32);
            parameters.Add("@Scored", goal.Scored, DbType.Boolean);

            return await base.SaveItem("FB_SavePenaltyShootoutGoal", parameters);
        }

        public async Task<bool> DeleteItem(PenaltyShootoutGoal goal)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PenaltyCode", goal.PenaltyCode, DbType.Int32);

            return await base.SaveItem("FB_DeletePenaltyShootoutGoal", parameters);
        }
      
    }
}