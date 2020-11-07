using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SamSmithNZ.Service.Models.FooFighters;
using SamSmithNZ.Web.Models.FooFighters;
using SamSmithNZ.Web.Services;
using SamSmithNZ.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class FooFightersController : Controller
    {
        private readonly IFooFightersServiceAPIClient _ServiceApiClient;
        private readonly IConfiguration _configuration;

        public FooFightersController(IFooFightersServiceAPIClient ServiceApiClient, IConfiguration configuration)
        {
            _ServiceApiClient = ServiceApiClient;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Song> items = await _ServiceApiClient.GetSongs();
            return View(items);
        }

        public async Task<IActionResult> ShowHistory(int yearCode = 0)
        {
            List<Year> years = await _ServiceApiClient.GetYears();
            List<AverageSetlist> averageSetlists = await _ServiceApiClient.GetAverageSetlist(yearCode);
            List<Show> shows = await _ServiceApiClient.GetShowsByYear(yearCode);

            return View(new ShowHistoryViewModel(years)
            {
                YearCode = yearCode,
                AverageSetlists = averageSetlists,
                Shows = shows
            });
        }

        [HttpPost]
        public IActionResult ShowHistoryYearPost(int yearCode)
        {
            if (yearCode > 0)
            {
                return RedirectToAction("ShowHistory", new { yearCode = yearCode });
            }

            return View(yearCode);
        }

        public async Task<IActionResult> Song(int songCode = 0, int? songkey = null)
        {
            //for backward compatibility
            if (songkey != null)
            {
                songCode = (int)songkey;
            }
            Song song = await _ServiceApiClient.GetSong(songCode);
            List<Show> shows = await _ServiceApiClient.GetShowsBySong(songCode);

            return View(new SongViewModel
            {
                Song = song,
                Shows = shows
            });
        }

        public async Task<IActionResult> Show(int showCode = 0, int? showkey = null)
        {
            //for backward compatibility
            if (showkey != null)
            {
                showCode = (int)showkey;
            }
            Show show = await _ServiceApiClient.GetShow(showCode);
            List<Song> songs = await _ServiceApiClient.GetSongsByShow(showCode);

            return View(new ShowViewModel
            {
                Show = show,
                Songs = songs
            });
        }

        public async Task<IActionResult> Album(int albumCode = 0, int? albumkey = null)
        {
            //for backward compatibility
            if (albumkey != null)
            {
                albumCode = (int)albumkey;
            }
            Album album = await _ServiceApiClient.GetAlbum(albumCode);
            List<Song> songs = await _ServiceApiClient.GetSongsByAlbum(albumCode);

            return View(new AlbumViewModel
            {
                Album = album,
                Songs = songs
            });
        }

        public async Task<IActionResult> AlbumList()
        {
            List<Album> albums = await _ServiceApiClient.GetAlbums();
            return View(albums);
        }

        public IActionResult ShowYearList()
        {
            return RedirectToAction("ShowList");
        }
    }
}
