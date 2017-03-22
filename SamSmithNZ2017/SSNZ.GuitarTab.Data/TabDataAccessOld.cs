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
        public List<Tab> GetData(short albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);

            return base.GetList("Tab_GetTracks", parameters).ToList<Tab>();
        }

        public Tab GetItem(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", trackCode, DbType.Int32);

            return base.GetItem("Tab_GetTracks", parameters);
        }

        public bool SaveItem(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", item.TrackCode, DbType.Int32);
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@TrackName", item.TrackName, DbType.String);
            parameters.Add("@TrackText", item.TrackText, DbType.String);
            parameters.Add("@TrackOrder", item.TrackOrder, DbType.Int32);
            parameters.Add("@Rating", item.Rating, DbType.Int32);
            parameters.Add("@TuningCode", item.TuningCode, DbType.Int32);

            return base.PostItem("Tab_SaveTrack", parameters);
        }

        public bool DeleteItem(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", trackCode, DbType.Int32);

            return base.PostItem("Tab_DeleteTrack", parameters);
        }

    }
}


