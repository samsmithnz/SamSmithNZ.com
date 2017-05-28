using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using SamSmithNZ2015.Core.iTunes;
//using SamSmithNZ2015.Models.iTunes;

namespace SamSmithNZ2017.Controllers
{
    public class iTunesController : Controller
    {
        //
        // GET: /iTunes/

        public ActionResult Index()
        {
            return View();
            //return Redirect("http://samsmithnz2015.azurewebsites.net/iTunes/Index");
            //SamSmithNZ2015.Core.iTunes.DataAccess.TopArtistsDataAccess b = new SamSmithNZ2015.Core.iTunes.DataAccess.TopArtistsDataAccess();
            //IList<TopArtists> TopArtistsList = b.GetItems(true);

            //SamSmithNZ2015.Core.iTunes.DataAccess.MovementDataAccess c = new SamSmithNZ2015.Core.iTunes.DataAccess.MovementDataAccess();
            //IList<Movement> MovementList = c.GetItems(true);

            ////SamSmithNZ2015.Core.iTunes.DataAccess.TrackDataAccess d = new SamSmithNZ2015.Core.iTunes.DataAccess.TrackDataAccess();
            ////IList<Track> Tracklist = d.GetItems(playlistCode, showJustSummary);

            //return View(new PlaylistDetailViewModel(null, TopArtistsList, MovementList, null));
        }

        public ActionResult PlayList(int playlistCode, Boolean showJustSummary)
        {
            return View();
        }

        public ActionResult PlaylistList()
        {
            return RedirectToAction("Index");

            //return Redirect("http://samsmithnz2015.azurewebsites.net/iTunes/Index");
            //SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess t = new SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess();
            //IList<Playlist> PlaylistList = t.GetItems();

            //return View(PlaylistList);
        }

        //[ChildActionOnly]
        //public ActionResult PlaylistSideBar()
        //{
        //    return Redirect("http://samsmithnz2015.azurewebsites.net/iTunes/Index");
        //    //SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess t = new SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess();
        //    //IList<Playlist> PlaylistList = t.GetItems();

        //    //// Return partial view
        //    //return PartialView(PlaylistList);
        //}

        public ActionResult PlaylistDetail(short playlistCode, Boolean showJustSummary)
        {
            return RedirectToAction("Playlist", new { playlistCode = playlistCode, showJustSummary = showJustSummary });
            //return Redirect("http://samsmithnz2015.azurewebsites.net/iTunes/Index");
            //SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess a = new SamSmithNZ2015.Core.iTunes.DataAccess.PlaylistDataAccess();
            //Playlist PlaylistRecord = a.GetItem(playlistCode);

            //SamSmithNZ2015.Core.iTunes.DataAccess.TopArtistsDataAccess b = new SamSmithNZ2015.Core.iTunes.DataAccess.TopArtistsDataAccess();
            //IList<TopArtists> TopArtistsList = b.GetItems(playlistCode, showJustSummary);

            //SamSmithNZ2015.Core.iTunes.DataAccess.MovementDataAccess c = new SamSmithNZ2015.Core.iTunes.DataAccess.MovementDataAccess();
            //IList<Movement> MovementList = c.GetItems(playlistCode, showJustSummary);

            //SamSmithNZ2015.Core.iTunes.DataAccess.TrackDataAccess d = new SamSmithNZ2015.Core.iTunes.DataAccess.TrackDataAccess();
            //IList<Track> Tracklist = d.GetItems(playlistCode, showJustSummary);

            //return View(new PlaylistDetailViewModel(PlaylistRecord, TopArtistsList, MovementList, Tracklist));
        }

    }
}
