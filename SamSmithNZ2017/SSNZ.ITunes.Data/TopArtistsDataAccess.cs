using Dapper;
using SSNZ.ITunes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.ITunes.Data
{
    public class TopArtistsDataAccess : GenericDataAccess<TopArtists>
    {
        public async Task<List<TopArtists>> GetListAsync(int playlistCode, Boolean showJustSummary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@playlist_code", playlistCode, DbType.Int32);
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("spITunes_GetTopArtists", parameters);
        }
     
        public async Task<List<TopArtists>> GetListAsync(Boolean showJustSummary)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@show_just_summary", showJustSummary, DbType.Boolean);

            return await base.GetListAsync("spITunes_GetTopArtists", parameters);
        }  

    }
}