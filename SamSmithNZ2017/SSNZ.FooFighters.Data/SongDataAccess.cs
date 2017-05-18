using Dapper;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.FooFighters.Data
{
    public class SongDataAccess : GenericDataAccess<Song>
    {
        public async Task<List<Song>> GetListAsync()
        {
            return await base.GetListAsync("FFL_GetSongs");
        }

        public async Task< List<Song>> GetListForAlbumAsync(int albumKey)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@album_key", albumKey, DbType.Int32);

            return await base.GetListAsync("FFL_GetSongs", parameters);
        }

        public async Task<List<Song>> GetListForShowAsync(int showKey)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@show_key", showKey, DbType.Int32);

            return await base.GetListAsync("FFL_GetSongs", parameters);
        }

        public async Task<Song> GetItemAsync(int songKey)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@song_key", songKey, DbType.Int32);

            return await base.GetItemAsync("FFL_GetSongs", parameters);
        }
    }
}


