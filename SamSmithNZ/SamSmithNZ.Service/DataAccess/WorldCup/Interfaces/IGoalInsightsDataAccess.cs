using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IGoalInsightsDataAccess
    {
        Task<List<GoalInsight>> GetList(bool analyzeExtraTime);        
    }
}