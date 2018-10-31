using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Web.Controllers
{
    public class SteamController : Controller
    {
        //https://steamcommunity.com/dev
        //https://developer.valvesoftware.com/wiki/Steam_Web_API#GetPlayerSummaries_.2v0001.29
        //https://portablesteamwebapi.codeplex.com/documentation
        //
        // GET: Steam
        //public ActionResult Index(string steamID)
        //{
        //    return Redirect("http://steamapiweb.azurewebsites.net/Home/Index?steamID="+ steamID);
        //}

        //public ActionResult SteamIsDown()
        //{
        //    return Redirect("http://steamapiweb.azurewebsites.net/Home/SteamIsDown");
        //}

        //public ActionResult About()
        //{
        //    return Redirect("http://steamapiweb.azurewebsites.net/Home/About");
        //}

        //public ActionResult GameDetails(string steamID, string appID)//, string currentCompletedFilter, string currentIncludeFilter, string currentExcludeFilter)
        //{
        //    return Redirect("http://steamapiweb.azurewebsites.net/Home/GameDetails?steamID=" + steamID + "&appID="+ appID);
        //}

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

        public ActionResult GameDetails(string steamID, string appID)
        {
            return View();
        }

    }
}