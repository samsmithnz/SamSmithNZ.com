using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SSNZ.GuitarTab.Service.Controllers
{
    public class AlbumController : ApiController
    {
        public async Task<List<Album>> GetAlbums(bool isAdmin)
        {
            AlbumDataAccess da = new AlbumDataAccess();
            return await da.GetListAsync(isAdmin);
        }

        public async Task<Album> GetAlbum(int albumCode, bool isAdmin)
        {
            AlbumDataAccess da = new AlbumDataAccess();
            return await da.GetItemAsync(albumCode, isAdmin);
        }

        public async Task<Album> SaveAlbum(Album item)
        {
            AlbumDataAccess da = new AlbumDataAccess();
            return await da.SaveItemAsync(item);
        }
    }
}
