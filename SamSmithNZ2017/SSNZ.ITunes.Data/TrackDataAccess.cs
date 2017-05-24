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
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("ITunes_GetTracks", parameters);
        }

        public async Task<Track> GetListAsync(int playlistCode, Boolean showJustSummary, String trackName)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);
            parameters.Add("@TrackName", trackName, DbType.String);

            return await base.GetItemAsync("ITunes_GetTracks", parameters);
        }
         
    }
}