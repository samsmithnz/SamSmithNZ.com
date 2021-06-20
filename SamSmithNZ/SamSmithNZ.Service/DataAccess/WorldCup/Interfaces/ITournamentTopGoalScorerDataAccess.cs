using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup.Interfaces
{
    public interface ITournamentTopGoalScorerDataAccess 
    {
        Task<List<TournamentTopGoalScorer>> GetList(int tournamentCode, bool getOwnGoals);

    }
}