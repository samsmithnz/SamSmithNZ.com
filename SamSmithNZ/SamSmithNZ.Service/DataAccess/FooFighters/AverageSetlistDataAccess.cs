using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.DataAccess.FooFighters.Interfaces;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public class AverageSetlistDataAccess : BaseDataAccess<AverageSetlist>, IAverageSetlistDataAccess
    {
        public AverageSetlistDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<AverageSetlist>> GetList(int yearCode, int minimumSongCount, bool showAllSongs)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@YearCode", yearCode, DbType.Int32);
            parameters.Add("@ShowMinimumSongCount", minimumSongCount, DbType.Int32);
            parameters.Add("@ShowAllSongs", showAllSongs, DbType.Boolean);

            return await base.GetList("FFL_GetAverageSetlist", parameters);
        }

    }
}


