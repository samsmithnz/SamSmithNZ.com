using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class GamePreEloRatingDataAccess : BaseDataAccess<GamePreEloRating>, IGamePreELORatingDataAccess
    {
        public GamePreEloRatingDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<GamePreEloRating> GetGamePreELORatings(int tournamentCode, int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add(name: "@GameCode", gameCode, DbType.Int32);

            GamePreEloRating results = await base.GetItem("FB_GetGamePreELORatings", parameters);
            return results;
        }        
        
    }
}