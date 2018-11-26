using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SamSmithNZ2017.Web.Controllers
{
    public class LegoController : Controller
    {
        // GET: Lego
        public ActionResult Index()
        {
            ViewBag.Title = "My Lego sets";
            return View();
        }
        public ActionResult Set(string setNum)
        {
            ViewBag.Title = "Lego set parts";
            return View(setNum);
        }
    }
}