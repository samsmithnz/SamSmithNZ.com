using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.Service.Controllers
{
    public class Old : ApiController
    {
        public List<Album> GetAlbums(bool isAdmin)
        {
            AlbumDataAccessOld da = new AlbumDataAccessOld();
            return da.GetData(isAdmin);
        }

        public Album GetAlbum(int albumCode, bool isAdmin)
        {
            AlbumDataAccessOld da = new AlbumDataAccessOld();
            return da.GetItem(albumCode, isAdmin);
        }

        public Album SaveAlbum(Album item)
        {
            AlbumDataAccessOld da = new AlbumDataAccessOld();
            return da.SaveItem(item);
        }
    }
}
