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
                //return RedirectToAction("Index", "Steam");
                return View();
            }
            else
            { 
                return Redirect("http://samsmithnz2015.azurewebsites.net");
            }
            //
        }

        public ActionResult About()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //return RedirectToAction("Index", "Steam");
                return View();
            }
            else
            {
                return Redirect("http://samsmithnz2015.azurewebsites.net/Home/About");
            }
            //ViewBag.Message = "Your application description page.";

            //return View();
        }

        public ActionResult Contact()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                //return RedirectToAction("Index", "Steam");
                return View();
            }
            else
            {
                return Redirect("http://samsmithnz2015.azurewebsites.net/Home/Contact");
            }
            //ViewBag.Message = "Your contact page.";

            //return View();
        }

        public ActionResult WpAdmin()
        {
            return View();
        }
    }
}