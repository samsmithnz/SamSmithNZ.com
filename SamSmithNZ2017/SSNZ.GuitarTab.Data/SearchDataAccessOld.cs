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
            parameters.Add("@record_id", recordid, DbType.Guid);

            return base.GetList("spKS_Tab_GetSearchResults", parameters).ToList<Search>();
        }

        public Guid SaveItem(String searchText)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@search_text", searchText, DbType.String);

            return base.GetScalar<Guid>("spKS_Tab_SaveSearchParameters", parameters);
        }

    }
}
