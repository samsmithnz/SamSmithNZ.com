using System;
using System.Web.Mvc;

namespace SamSmithNZ2017.Controllers
{
    public class iTunesController : Controller
    {
        //
        // GET: /iTunes/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlayList(int playlistCode, Boolean showJustSummary)
        {
            return View();
        }

        public ActionResult PlaylistList()
        {
            return RedirectToAction("Index");
        }

        public ActionResult PlaylistDetail(short playlistCode, Boolean showJustSummary)
        {
            return RedirectToAction("Playlist", new { playlistCode = playlistCode, showJustSummary = showJustSummary });
        }

    }
}
