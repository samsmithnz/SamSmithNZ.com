using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Web.Controllers
{
    public class SteamOldController : Controller
    {

        // GET: /SteamOld/
        public ActionResult Index(string steamID)
        {
            return RedirectToAction("SteamIsDown", "Steam", new { steamID = steamID });
        }

        public ActionResult SteamIsDown()
        {
            return RedirectToAction("SteamIsDown", "Steam");
        }

        public ActionResult About()
        {
            return RedirectToAction("About", "Steam");
        }

        public ActionResult GameDetails(string steamID, string appID, string currentCompletedFilter, string currentIncludeFilter, string currentExcludeFilter)
        {
            return RedirectToAction("GameDetails", "Steam", new { steamID = steamID, appID = appID });
        }

    }
}