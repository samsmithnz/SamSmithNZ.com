using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.WorldCup.Interfaces;
using SamSmithNZ.Service.Models.WorldCup;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.WorldCup
{
    public class PlayoffSetupDataAccess : BaseDataAccess<PlayoffSetup>, IPlayoffSetupDataAccess
    {
        public PlayoffSetupDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }
    
        public async Task<bool> SaveItem(PlayoffSetup setup)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", setup.TournamentCode, DbType.Int32);
            parameters.Add("@RoundCode", setup.RoundCode, DbType.String);
            parameters.Add("@GameNumber", setup.GameNumber, DbType.Int32);
            parameters.Add("@Team1Prereq", setup.Team1Prereq, DbType.String);
            parameters.Add("@Team2Prereq", setup.Team2Prereq, DbType.String);

            await base.SaveItem("FB_SaveMigratePlayoffsGames", parameters);
            return true;
        }

    }
}