using Dapper;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SSNZ.FooFighters.Data
{
    public class AlbumDataAccess : GenericDataAccess<Album>
    {
        public async Task<List<Album>> GetListAsync()
        {
            return await base.GetListAsync("FFL_GetAlbums");
        }

        public async Task<Album> GetItemAsync(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@albumCode", albumCode, DbType.Int32);

            return await base.GetItemAsync("FFL_GetAlbums", parameters);
        }
    }
}


