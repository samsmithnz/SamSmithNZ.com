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
    public class PlayerDataAccess : BaseDataAccess<Player>, IPlayerDataAccess
    {
        public PlayerDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Player>> GetList(int gameCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@GameCode", gameCode, DbType.Int32);

            return await base.GetList("FB_GetPlayers", parameters);
        }

        public async Task<bool> SaveItem(Player player)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TeamCode", player.TeamCode, DbType.Int32);
            parameters.Add("@TournamentCode", player.TournamentCode, DbType.Int32);
            parameters.Add("@Number", player.Number, DbType.Int32);
            parameters.Add("@Position", player.Position, DbType.String);
            parameters.Add("@IsCaptain", player.IsCaptain, DbType.Boolean);
            parameters.Add("@PlayerName", player.PlayerName, DbType.String);
            parameters.Add("@DateOfBirth", player.DateOfBirth, DbType.DateTime);
            parameters.Add("@ClubName", player.ClubName, DbType.String);

            return await base.SaveItem("FB_SavePlayer", parameters);
        }

    }
}