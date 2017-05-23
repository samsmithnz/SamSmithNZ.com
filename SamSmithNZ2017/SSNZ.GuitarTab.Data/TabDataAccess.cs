using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.Data
{
    public class TabDataAccess : GenericDataAccess<Tab>
    {
        public async Task<List<Tab>> GetListAsync(int albumCode, int sortOrder)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);
            parameters.Add("@SortOrder", sortOrder, DbType.Int32);

            List<Tab> result = await base.GetListAsync("Tab_GetTabs", parameters);

            return result.ToList<Tab>();
        }

        public async Task<Tab> GetItemAsync(int tabCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", tabCode, DbType.Int32);

            List<Tab> results = await base.GetListAsync("Tab_GetTabs", parameters);

            //Sometimes a tab can be associated with multiple albums, so we need to get a list and return the first record in those cases
            if (results.Count > 0)
            {
                return results[0];
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> SaveItemAsync(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", item.TabCode, DbType.Int32);
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@TabName", item.TabName, DbType.String);
            parameters.Add("@TabText", item.TabText, DbType.String);
            parameters.Add("@TabOrder", item.TabOrder, DbType.Int32);
            parameters.Add("@Rating", item.Rating, DbType.Int32);
            parameters.Add("@TuningCode", item.TuningCode, DbType.Int32);

            return await base.PostItemAsync("Tab_SaveTab", parameters);
        }

        public async Task<bool> DeleteItemAsync(int tabCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", tabCode, DbType.Int32);

            return await base.PostItemAsync("Tab_DeleteTab", parameters);
        }

    }
}


