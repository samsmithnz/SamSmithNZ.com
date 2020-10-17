using Dapper;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.DataAccess.Base;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SamSmithNZ.Service.DataAccess.FooFighters
{
    public class AlbumDataAccess : BaseDataAccess<Album>, IAlbumDataAccess
    {
        public AlbumDataAccess(IConfiguration configuration)
        {
            base.SetupConnectionString(configuration);
        }

        public async Task<List<Album>> GetListAsync()
        {
            return await base.GetList("FFL_GetAlbums");
        }

        public async Task<Album> GetItemAsync(int albumCode)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@albumCode", albumCode, DbType.Int32);

            return await base.GetItem("FFL_GetAlbums", parameters);
        }
    }
}


