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
            parameters.Add("@year_code", yearCode, DbType.Int32);

            return await base.GetListAsync("FFL_GetShows", parameters);
        }

        public async Task<List<Show>> GetListBySongAsync(int songKey)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@song_key", songKey, DbType.Int32);

            return await base.GetListAsync("FFL_GetShows", parameters);
        }

        public async Task<Show> GetItemAsync(int showKey)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@show_key", showKey, DbType.Int32);

            return await base.GetItemAsync("FFL_GetShows", parameters);
        }
    }
}


