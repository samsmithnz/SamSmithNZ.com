using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                return RedirectToAction("Index", "Steam");
            }
            else
            { 
                return Redirect("http://samsmithnz.com");
            }
            //return View();
        }

        public ActionResult About()
        {
            return Redirect("http://samsmithnz.com/Home/About");
            //ViewBag.Message = "Your application description page.";

            //return View();
        }

        public ActionResult Contact()
        {
            return Redirect("http://samsmithnz.com/Home/Contact");
            //ViewBag.Message = "Your contact page.";

            //return View();
        }
    }
}