using Dapper;
using SSNZ.ITunes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.ITunes.Data
{
    public class PlaylistDataAccess : GenericDataAccess<Playlist>
    {
        public async Task<List<Playlist>> GetListAsync()
        {
            DynamicParameters parameters = new DynamicParameters();

            return await base.GetListAsync("ITunes_GetPlaylists", parameters);
        }
    
        public async Task<Playlist> GetItem(int playlistCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PlaylistCode", playlistCode, DbType.Int32);

            return await base.GetItemAsync("ITunes_GetPlaylists", parameters);
        }
      
    }
}