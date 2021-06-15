using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;

namespace SamSmithNZ.Web.Models.WorldCup
{
    public class InsightsViewModel
    {
        public InsightsViewModel()
        {
            this.RegularTimeGoalInsights = new();
            this.ExtraTimeGoalInsights = new();
        }

        public List<GoalInsight> RegularTimeGoalInsights { get; set; }
        public List<GoalInsight> ExtraTimeGoalInsights { get; set; }
    }
}