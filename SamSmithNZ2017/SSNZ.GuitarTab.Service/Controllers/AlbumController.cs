using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

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
