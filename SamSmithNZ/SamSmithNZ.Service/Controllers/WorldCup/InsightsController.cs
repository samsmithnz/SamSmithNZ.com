using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    [Route("api/WorldCup/[controller]")]
    [ApiController]
    public class InsightsController : ControllerBase
    {
        private readonly IGoalInsightsDataAccess _repo;

        public InsightsController(IGoalInsightsDataAccess repo)
        {
            _repo = repo;
        }

        [HttpGet("GetGoalInsights")]
        public async Task<List<GoalInsight>> GetGames(bool analyzeExtraTime)
        {
            return await _repo.GetList(analyzeExtraTime);
        }
    }
}
