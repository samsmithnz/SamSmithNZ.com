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
    public class StatsAverageTournamentGoalsDataAccess : BaseDataAccess<StatsAverageTournamentGoals>, IStatsAverageTournamentGoalsDataAccess
    {
        public StatsAverageTournamentGoalsDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<StatsAverageTournamentGoals>> GetList(int? competitionCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@CompetitionCode", competitionCode, DbType.Int32);

            return await base.GetList("FB_GetStatsAverageTournamentGoals", parameters);
        }

        public async Task<StatsAverageTournamentGoals> GetItem(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetItem("FB_GetStatsAverageTournamentGoals", parameters);
        }

    }
}
