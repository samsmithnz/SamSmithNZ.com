using Microsoft.AspNetCore.Mvc;

namespace SamSmithNZ.Service.Controllers.WorldCup
{
    public class TournamentOddsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
