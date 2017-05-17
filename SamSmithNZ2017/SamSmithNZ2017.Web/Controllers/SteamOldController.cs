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
            return RedirectToAction("Steam", "Index", new { steamID = steamID });
        }

        public ActionResult SteamIsDown()
        {
            return RedirectToAction("Steam", "SteamIsDown");
        }

        public ActionResult About()
        {
            return RedirectToAction("Steam", "About");
        }

        public ActionResult GameDetails(string steamID, string appID, string currentCompletedFilter, string currentIncludeFilter, string currentExcludeFilter)
        {
            return RedirectToAction("Steam", "GameDetails", new { steamID = steamID, appID = appID });
        }

    }
}