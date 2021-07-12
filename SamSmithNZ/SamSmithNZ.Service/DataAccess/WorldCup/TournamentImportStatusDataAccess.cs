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
    public class TournamentImportStatusDataAccess : BaseDataAccess<TournamentImportStatus>, ITournamentImportStatusDataAccess
    {
        public TournamentImportStatusDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<TournamentImportStatus>> GetList(int? competitionCode)
        {
            DynamicParameters parameters = new();
            parameters.Add("@CompetitionCode", competitionCode, DbType.Int32);

            return await base.GetList("FB_GetTournamentsImportStatus", parameters);
        }

    }
}