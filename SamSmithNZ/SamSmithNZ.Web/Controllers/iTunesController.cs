using Microsoft.AspNetCore.Mvc;
using SamSmithNZ.Service.Models.ITunes;
using SamSmithNZ.Web.Models.ITunes;
using SamSmithNZ.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class ITunesController : Controller
    {
        private readonly IITunesServiceApiClient _ServiceApiClient;

        public ITunesController(IITunesServiceApiClient ServiceApiClient  )
        {
            _ServiceApiClient = ServiceApiClient;
        }

        public async Task<IActionResult> Index()
        {
            bool showJustSummary = true;
            List<TopArtists> topArtists = await _ServiceApiClient.GetTopArtistsSummary(showJustSummary);
            List<Playlist> playlists = await _ServiceApiClient.GetPlaylists(showJustSummary);

            return View(new IndexViewModel
            {
                TopArtists = topArtists,
                Playlists = playlists
            });
        }

        public async Task<IActionResult> PlayList(int playlistCode, bool showJustSummary = true)
        {
            Playlist playlist = await _ServiceApiClient.GetPlaylist(playlistCode);
            List<TopArtists> topArtists = await _ServiceApiClient.GetTopArtistsByPlaylist(playlistCode, showJustSummary);
            List<Movement> movements = await _ServiceApiClient.GetMovementsByPlaylist(playlistCode, showJustSummary);
            List<Track> tracks = await _ServiceApiClient.GetTracks(playlistCode, showJustSummary);

            return View(new PlaylistViewModel
            {
                Playlist = playlist,
                TopArtists = topArtists,
                Movements = movements,
                Tracks = tracks
            });
        }

        public IActionResult About()
        {
            return RedirectToAction("About", "Home");
        }
    }
}
