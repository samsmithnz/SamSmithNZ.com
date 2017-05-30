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

        public async Task< List<Song>> GetListForAlbumAsync(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@albumCode", albumCode, DbType.Int32);

            return await base.GetListAsync("FFL_GetSongs", parameters);
        }

        public async Task<List<Song>> GetListForShowAsync(int showCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@showCode", showCode, DbType.Int32);

            return await base.GetListAsync("FFL_GetSongs", parameters);
        }

        public async Task<Song> GetItemAsync(int songCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@songCode", songCode, DbType.Int32);

            return await base.GetItemAsync("FFL_GetSongs", parameters);
        }
    }
}


