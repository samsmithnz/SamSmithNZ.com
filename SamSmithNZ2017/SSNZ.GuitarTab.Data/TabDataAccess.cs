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
        public async Task<List<Tab>> GetListAsync(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);

            List<Tab> result = await base.GetListAsync("Tab_GetTracks", parameters);

            return result.ToList<Tab>();
        }

        public async Task<Tab> GetItemAsync(int trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", trackCode, DbType.Int32);

            return await base.GetItemAsync("Tab_GetTracks", parameters);
        }

        public async Task<bool> SaveItemAsync(Tab item)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", item.TrackCode, DbType.Int32);
            parameters.Add("@AlbumCode", item.AlbumCode, DbType.Int32);
            parameters.Add("@TrackName", item.TrackName, DbType.String);
            parameters.Add("@TrackText", item.TrackText, DbType.String);
            parameters.Add("@TrackOrder", item.TrackOrder, DbType.Int32);
            parameters.Add("@Rating", item.Rating, DbType.Int32);
            parameters.Add("@TuningCode", item.TuningCode, DbType.Int32);

            return await base.PostItemAsync("Tab_SaveTrack", parameters);
        }

        public async Task<bool> DeleteItemAsync(int trackCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@TrackCode", trackCode, DbType.Int32);

            return await base.PostItemAsync("Tab_DeleteTrack", parameters);
        }

    }
}


