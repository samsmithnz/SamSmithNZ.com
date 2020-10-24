using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Web.Models.GuitarTab;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class GuitarTabController : Controller
    {
        private readonly IGuitarTabServiceAPIClient _ServiceApiClient;
        private readonly IConfiguration _configuration;

        public GuitarTabController(IGuitarTabServiceAPIClient ServiceApiClient, IConfiguration configuration)
        {
            _ServiceApiClient = ServiceApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Artist> artists = await _ServiceApiClient.GetArtists();
            List<Album> albums = await _ServiceApiClient.GetAlbums();
            List<KeyValuePair<Artist, List<Album>>> items = new List<KeyValuePair<Artist, List<Album>>>();
            foreach (Artist artist in artists)
            {
                List<Album> artistAlbums = (albums.Where(a => a.ArtistName == artist.ArtistName)).ToList<Album>();
                KeyValuePair<Artist, List<Album>> newItem = new KeyValuePair<Artist, List<Album>>(artist, artistAlbums);
                items.Add(newItem);
            }

            return View(items);
        }

        public ActionResult About()
        {
            return View();
        }

        public async Task<IActionResult> Album(int albumCode)
        {
            Album album = await _ServiceApiClient.GetAlbum(albumCode);
            List<Tab> tabs = await _ServiceApiClient.GetTabs(albumCode);

            return View(new AlbumViewModel()
            {
                Album = album,
                Tabs = tabs
            });
        }

        public IActionResult SearchResults(string searchText)
        {
            return View();
        }

        public IActionResult EditAlbum(int albumCode)
        {
            return View();
        }

        public IActionResult EditTab(int tabCode)
        {
            return View();
        }

        public IActionResult AlbumTabList(int albumCode)
        {
            return RedirectToAction("Album", new { albumCode = albumCode });
        }

    }
}
