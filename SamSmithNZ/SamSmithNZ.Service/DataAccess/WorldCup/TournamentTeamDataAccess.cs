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
    public class TournamentTeamDataAccess : BaseDataAccess<TournamentTeam>, ITournamentTeamDataAccess
    {
        public TournamentTeamDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<TournamentTeam>> GetQualifiedTeams(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetList("FB_GetTournamentTeams", parameters);
        }

        public async Task<List<TournamentTeam>> GetTeamsPlacingAsync(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetList("FB_GetTournamentTeamsPlacing", parameters);
        }

        public async Task<TournamentTeam> GetTournamentTeamAsync(int tournamentCode, int teamCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", teamCode, DbType.Int32);

            return await base.GetItem("FB_GetTournamentTeams", parameters);
        }

        public async Task<bool> SaveItem(TournamentTeam tournamentTeam)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentTeam.TournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", tournamentTeam.TeamCode, DbType.Int32);

            return await base.SaveItem("FB_SaveTournamentTeam", parameters);
        }

        public async Task<bool> DeleteItem(TournamentTeam tournamentTeam)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentTeam.TournamentCode, DbType.Int32);
            parameters.Add("@TeamCode", tournamentTeam.TeamCode, DbType.Int32);

            return await base.SaveItem("FB_DeleteTournamentTeam", parameters);
        }

    }
}