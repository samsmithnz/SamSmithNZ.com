using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using SSNZ.ITunes.Data;
using SSNZ.ITunes.Models;

namespace SSNZ.ITunes.ImporterWin
{
    public static class DataAccess
    {

        public async static Task<int> CreateNewPlayList(DateTime newPlaylistDate)
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            return await da.SaveItemAsync(newPlaylistDate);
        }

        public async static Task<List<Playlist>> GetPlayLists()
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            return await da.GetListAsync();
        }

        public async static Task<bool> DeletePlaylistTracks(int playListCode)
        {
            PlaylistDataAccess da = new PlaylistDataAccess();
            return await da.DeleteItemAsync(playListCode);
        }

        public async static Task<bool> InsertTrack(Track track)
        {
            TrackDataAccess da = new TrackDataAccess();
            return await da.SaveItemAsync(track);
        }

        public async static Task<List<Track>> ValidateTracksForDuplicates(int playlistCode)
        {
            TrackDataAccess da = new TrackDataAccess();
            return await da.ValidateTracksForPlaylistAsync(playlistCode);
        }

        public async static Task<bool> SetTrackRanks(int playlistCode)
        {
            TrackDataAccess da = new TrackDataAccess();
            return await da.SetTrackRanksForPlaylistAsync(playlistCode);
        }

    }
}