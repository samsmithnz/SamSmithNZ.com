using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSNZ.Steam.Web.Controllers
{
    public class HomeController : Controller
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation
        //
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