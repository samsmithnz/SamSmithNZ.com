using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.FooFighters.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.FooFighters.Data
{
    public class AverageSetlistDataAccess : GenericDataAccess<AverageSetlist>
    {
        public async Task<List<AverageSetlist>> GetListAsync(int yearCode, int minimumSongCount, bool showAllSongs)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@YearCode", yearCode, DbType.Int32);
            parameters.Add("@ShowMinimumSongCount", minimumSongCount, DbType.Int32);
            parameters.Add("@ShowAllSongs", showAllSongs, DbType.Boolean);

            List<AverageSetlist> result = await base.GetListAsync("FFL_GetAverageSetlist", parameters);

            return result.ToList<AverageSetlist>();
        }

    }
}


