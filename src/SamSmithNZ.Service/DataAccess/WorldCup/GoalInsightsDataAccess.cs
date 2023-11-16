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
    public class GoalInsightsDataAccess : BaseDataAccess<GoalInsight>, IGoalInsightsDataAccess
    {
        public GoalInsightsDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<GoalInsight>> GetList(bool analyzeExtraTime)
        {
            DynamicParameters parameters = new();
            parameters.Add("@AnalyzeExtraTime", analyzeExtraTime, DbType.Boolean);

            List<GoalInsight> results = await base.GetList("FB_GetGoalInsights", parameters);
            return results;
        }

    }
}