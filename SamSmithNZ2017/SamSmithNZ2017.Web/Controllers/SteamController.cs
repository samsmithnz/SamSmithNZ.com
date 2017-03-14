using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Web.Controllers
{
    public class SteamController : Controller
    {
        // GET: Steam
        public ActionResult Index(string steamID)
        {
            return View();
        }

        public ActionResult SteamIsDown()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult GameDetails(string steamID, string appID)//, string currentCompletedFilter, string currentIncludeFilter, string currentExcludeFilter)
        {
            return View();
        }

    }
}