using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SSNZ.GuitarTab.Models;
using Dapper;
using System.Data;

namespace SSNZ.GuitarTab.Data
{
    public class SearchDataAccessOld : GenericDataAccessOld<Search>
    {
        public List<Search> GetData(Guid recordid)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@RecordId", recordid, DbType.Guid);

            return base.GetList("Tab_GetSearchResults", parameters).ToList<Search>();
        }

        public Guid SaveItem(String searchText)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchText, DbType.String);

            return base.GetScalar<Guid>("Tab_SaveSearchParameters", parameters);
        }

    }
}
