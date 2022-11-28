using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface IStatsAverageTournamentGoalsDataAccess
    {
        Task<List<StatsAverageTournamentGoals>> GetList(int? competitionCode);
        Task<StatsAverageTournamentGoals> GetItem(int tournamentCode);
    }
}
