using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class GamePreELORatingDataAccess : BaseDataAccess<GamePreELORating>, IGamePreELORatingDataAccess
    {
        public GamePreELORatingDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<GamePreELORating> GetGamePreELORatings(int tournamentCode, int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add(name: "@GameCode", gameCode, DbType.Int32);

            GamePreELORating results = await base.GetItem("FB_GetGamePreELORatings", parameters);
            return results;
        }        
        
    }
}