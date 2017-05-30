using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.FooFighters.Service.Controllers
{
    public class AlbumController : ApiController
    {
        public async Task<List<Album>> GetAlbums()
        {
            AlbumDataAccess da = new AlbumDataAccess();
            return await da.GetListAsync();
        }

        public async Task<Album> GetAlbum(int albumCode)
        {
            AlbumDataAccess da = new AlbumDataAccess();
            return await da.GetItemAsync(albumCode);
        }

    }
}
