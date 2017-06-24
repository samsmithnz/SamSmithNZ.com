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

        //public async Task<Track> GetItemAsync(int playlistCode, Boolean showJustSummary, String trackName)
        //{
        //    DynamicParameters parameters = new DynamicParameters();
        //    parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
        //    parameters.Add("@ShowJustSummary", showJustSummary, DbType.Boolean);
        //    parameters.Add("@TrackName", trackName, DbType.String);

        //    return await base.GetItemAsync("ITunes_GetTracks", parameters);
        //}

        public async Task<bool> SaveItemAsync(Track track)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PlaylistCode", track.PlaylistCode, DbType.Int32);
            parameters.Add("@TrackName", track.TrackName, DbType.String);
            parameters.Add("@AlbumName", track.AlbumName, DbType.String);
            parameters.Add("@ArtistName", track.ArtistName, DbType.String);
            parameters.Add("@PlayCount", track.PlayCount, DbType.Int32);
            parameters.Add("@Ranking", track.Ranking, DbType.Int32);
            parameters.Add("@Rating", track.Rating, DbType.Int32);

            return await base.PostItemAsync("ITunes_ImportInsertTrack", parameters);
        }

        public async Task<List<Track>> ValidateTracksForPlaylistAsync(int playlistCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            int timeOut = 3600; //One hour

            return await base.GetListAsync("ITunes_ImportValidateTracksForDuplicates", parameters, timeOut);
        }

        public async Task<bool> SetTrackRanksForPlaylistAsync(int playlistCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);
            int timeOut = 3600; //One hour

            return await base.PostItemAsync("ITunes_ImportSetTrackRanks", parameters, timeOut);
        }

    }
}