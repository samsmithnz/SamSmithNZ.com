using Dapper;
using SSNZ.ITunes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.ITunes.Data
{
    public class TrackDataAccess : GenericDataAccess<Track>
    {
        public async Task<List<Track>> GetListAsync(int playlistCode, Boolean showJustSummary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@playlist_code", playlistCode, DbType.Int32);
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("spITunes_GetTracks", parameters);
        }

        public async Task<Track> GetListAsync(int playlistCode, Boolean showJustSummary, String trackName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@playlist_code", playlistCode, DbType.Int32);
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);
            parameters.Add("@track_name", trackName, DbType.String);

            return await base.GetItemAsync("spITunes_GetTracks", parameters);
        }
         
    }
}