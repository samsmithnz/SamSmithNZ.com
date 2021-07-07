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
    public class TournamentDataAccess : BaseDataAccess<Tournament>, ITournamentDataAccess
    {
        public TournamentDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Tournament>> GetList(int? competitionCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@CompetitionCode", competitionCode, DbType.Int32);

            return await base.GetList("FB_GetTournaments", parameters);
        }

        public async Task<Tournament> GetItem(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.GetItem("FB_GetTournaments", parameters);
        }

        public async Task<bool> ResetTournament(int tournamentCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@TournamentCode", tournamentCode, DbType.Int32);

            return await base.SaveItem("FB_ResetTournament", parameters);
        }

    }
}