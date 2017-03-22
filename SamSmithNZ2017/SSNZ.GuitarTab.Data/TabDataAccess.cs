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
    public class TabDataAccess : GenericDataAccess<Tab>//, IStandardRepository<Track, Guid>
    {
        public List<Tab> GetItems(short albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_code", albumCode, DbType.Int16);

            return base.GetList("spKS_Tab_GetTracks", parameters).ToList<Tab>();
        }

        public Tab GetItem(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", trackCode, DbType.Int16);

            return base.GetItem("spKS_Tab_GetArtists", parameters);
        }

        public bool Save(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", item.TrackCode, DbType.Int32);
            parameters.Add("@album_code", item.AlbumCode, DbType.Int32);
            parameters.Add("@track_name", item.TrackName, DbType.String);
            parameters.Add("@track_text", item.TrackText, DbType.String);
            parameters.Add("@track_order", item.TrackOrder, DbType.Int32);
            parameters.Add("@rating", item.Rating, DbType.Int32);
            parameters.Add("@tuning_code", item.TuningCode, DbType.Int32);

            return base.PostItem("spKS_Tab_SaveTrack", parameters);
        }

        public bool Delete(short trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@track_code", trackCode, DbType.Int32);

            return base.PostItem("spKS_Tab_DeleteTrack", parameters);
        }

    }
}


