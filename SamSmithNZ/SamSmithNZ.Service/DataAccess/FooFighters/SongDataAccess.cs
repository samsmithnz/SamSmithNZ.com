using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public class SongDataAccess : BaseDataAccess<Song>, ISongDataAccess
    {
        public SongDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Song>> GetListAsync()
        {
            return await base.GetList("FFL_GetSongs");
        }

        public async Task<List<Song>> GetListForAlbumAsync(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@AlbumCode", albumCode, DbType.Int32);

            return await base.GetList("FFL_GetSongs", parameters);
        }

        public async Task<List<Song>> GetListForShowAsync(int showCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ShowCode", showCode, DbType.Int32);

            return await base.GetList("FFL_GetSongs", parameters);
        }

        public async Task<Song> GetItemAsync(int songCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SongCode", songCode, DbType.Int32);

            return await base.GetItem("FFL_GetSongs", parameters);
        }

        public async Task<bool> SaveItemAsync(int songCode, int showCode, int showSongOrder)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@SongCode", songCode, DbType.Int32);
            parameters.Add("@ShowCode", showCode, DbType.Int32);
            parameters.Add("@ShowSongOrder", showSongOrder, DbType.Int32);

            return await base.SaveItem("FFL_SaveShowSong", parameters);
        }
    }
}


