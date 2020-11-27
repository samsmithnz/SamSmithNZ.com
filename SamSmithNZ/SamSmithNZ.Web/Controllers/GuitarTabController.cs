using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.GuitarTab;
using SamSmithNZ.Web.Models.GuitarTab;
using SamSmithNZ.Web.Services.Interfaces;
using System;
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

        public async Task<IActionResult> Index(bool isAdmin = false)
        {
            List<Artist> artists = await _ServiceApiClient.GetArtists(isAdmin);
            List<Album> albums = await _ServiceApiClient.GetAlbums(isAdmin);
            List<KeyValuePair<Artist, List<Album>>> items = new List<KeyValuePair<Artist, List<Album>>>();
            foreach (Artist artist in artists)
            {
                List<Album> artistAlbums = (albums.Where(a => a.ArtistName == artist.ArtistName)).ToList<Album>();
                KeyValuePair<Artist, List<Album>> newItem = new KeyValuePair<Artist, List<Album>>(artist, artistAlbums);
                items.Add(newItem);
            }

            return View(new IndexViewModel
            {
                ArtistAlbums = items,
                IsAdmin = isAdmin
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }

        public async Task<IActionResult> Album(int albumCode, bool isAdmin = false)
        {
            Album album = await _ServiceApiClient.GetAlbum(albumCode, isAdmin);
            List<Tab> tabs = await _ServiceApiClient.GetTabs(albumCode);

            return View(new AlbumViewModel()
            {
                Album = album,
                Tabs = tabs,
                IsAdmin = isAdmin
            });
        }

        public async Task<IActionResult> SearchResults(string searchText)
        {
            List<Search> searchResults = await _ServiceApiClient.GetSearchResults(searchText);

            return View(new SearchViewModel
            {
                SearchResults = searchResults
            });
        }

        [HttpPost]
        public IActionResult SearchPost(string txtSearch)
        {
            return RedirectToAction("SearchResults",
               new
               {
                   searchText = txtSearch
               });
        }

        public async Task<IActionResult> EditAlbum(int albumCode)
        {
            Album album = await _ServiceApiClient.GetAlbum(albumCode, true);
            List<Tab> tabs = await _ServiceApiClient.GetTabs(albumCode);

            return View(new AlbumTabsViewModel
            {
                Album = album,
                Tabs = tabs
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveAlbum(int albumCode, string txtArtist, string txtAlbumName, string txtYear,
            bool chkIsBassTab, bool chkIncludeInIndex, bool chkIncludeOnWebsite, bool chkIsMiscCollectionAlbum)
            //string txtTrackList)
        {
            Album album = new Album
            {
                AlbumCode = albumCode,
                ArtistName = txtArtist,
                AlbumName = txtAlbumName,
                AlbumYear = int.Parse(txtYear),
                IsBassTab = chkIsBassTab,
                IncludeInIndex = chkIncludeInIndex,
                IncludeOnWebsite = chkIncludeOnWebsite,
                IsMiscCollectionAlbum = chkIsMiscCollectionAlbum
            };

            await _ServiceApiClient.SaveAlbum(album);

            return RedirectToAction("Album", new
            {
                albumCode = albumCode,
                isAdmin = true
            });
        }

        public async Task<IActionResult> EditTab(int tabCode)
        {

            Tab tab = await _ServiceApiClient.GetTab(tabCode);
            List<Rating> ratings = await _ServiceApiClient.GetRatings();
            List<Tuning> tunings = await _ServiceApiClient.GetTunings();

            return View(new TabsViewModel(ratings, tunings)
            {
                Tab = tab,
                Rating = tab.Rating == null ? "" : tab.Rating.ToString(),
                Tuning = tab.TuningCode.ToString()
            });
        }

        [HttpPost]
        public async Task<IActionResult> SaveTab(int tabCode, int albumCode, string txtTabName, string txtTabText, string txtOrder, string cboRating, string cboTuning)
        {
            Tab tab = new Tab
            {
                TabCode = tabCode,
                AlbumCode = albumCode,
                TabName = txtTabName,
                TabText = txtTabText,
                TabOrder = int.Parse(txtOrder),
                Rating = int.Parse(cboRating),
                TuningCode = int.Parse(cboTuning)
            };
            await _ServiceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = true
            });
        }

        public async Task<IActionResult> AddNewTrack(int albumCode)
        {
            //Get the current list of tabs, to establish the last position of the new tab
            List<Tab> tabs = await _ServiceApiClient.GetTabs(albumCode);

            Tab tab = new Tab
            {
                TabCode = 0,
                AlbumCode = albumCode,
                TabName = "Track " + (tabs.Count + 1).ToString(),
                TabText = "",
                TabOrder = (tabs.Count + 1),
                Rating = 0, // no rating
                TuningCode = 0 // no tuning
            };
            await _ServiceApiClient.SaveTab(tab);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = true
            });
        }

        public async Task<IActionResult> DeleteTab(int albumCode, int tabCode)
        {
            await _ServiceApiClient.DeleteTab(tabCode);

            return RedirectToAction("EditAlbum", new
            {
                albumCode = albumCode,
                isAdmin = true
            });
        }

    }
}
