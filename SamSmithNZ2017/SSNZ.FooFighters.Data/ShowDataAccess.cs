using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using SSNZ.FooFighters.Models;
using Dapper;
using System.Threading.Tasks;
using System.Data;

namespace SSNZ.FooFighters.Data
{
    public class ShowDataAccess : GenericDataAccess<Show>
    {

        public async Task<List<Show>> GetListByYearAsync(int yearCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@yearCode", yearCode, DbType.Int32);

            return await base.GetListAsync("FFL_GetShows", parameters);
        }

        public async Task<List<Show>> GetListBySongAsync(int songCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@songCode", songCode, DbType.Int32);

            return await base.GetListAsync("FFL_GetShows", parameters);
        }

        public async Task<Show> GetItemAsync(int showCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@showCode", showCode, DbType.Int32);

            return await base.GetItemAsync("FFL_GetShows", parameters);
        }
    }
}


