using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamSmithNZ.Web.Controllers
{
    public class SpamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
