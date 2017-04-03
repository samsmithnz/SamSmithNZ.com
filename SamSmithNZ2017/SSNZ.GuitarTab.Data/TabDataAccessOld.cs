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
    public class TabDataAccessOld : GenericDataAccessOld<Tab>
    {
        public List<Tab> GetData(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);

            return base.GetList("Tab_GetTabs", parameters).ToList<Tab>();
        }

        public Tab GetItem(int trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", trackCode, DbType.Int32);

            return base.GetItem("Tab_GetTabs", parameters);
        }

        public bool SaveItem(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", item.TabCode, DbType.Int32);
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@TabName", item.TabName, DbType.String);
            parameters.Add("@TabText", item.TabText, DbType.String);
            parameters.Add("@TabOrder", item.TabOrder, DbType.Int32);
            parameters.Add("@Rating", item.Rating, DbType.Int32);
            parameters.Add("@TuningCode", item.TuningCode, DbType.Int32);

            return base.PostItem("Tab_SaveTab", parameters);
        }

        public bool DeleteItem(int trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TabCode", trackCode, DbType.Int32);

            return base.PostItem("Tab_DeleteTab", parameters);
        }

    }
}


