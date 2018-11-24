using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SSNZ.Steam2019.Service.Controllers
{
    [EnableCors("MyCorsPolicy")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        // GET
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

    }
}
