using System.Web.Mvc;

namespace SamSmithNZ2017.Controllers
{
    public class GuitarTabController : Controller
    {
        //
        // GET: /GuitarTab/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Album(int albumCode)
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult SearchResults(string searchText)
        {
            return View();
        }

        [Authorize(Roles = "WebAdmin")]
        public ActionResult EditAlbum(int albumCode)
        {
            return View();
        }

        [Authorize(Roles = "WebAdmin")]
        public ActionResult EditTab(int tabCode)
        {
            return View();
        }

        public ActionResult AlbumTabList(int albumCode)
        {
            return RedirectToAction("Album", new { albumCode = albumCode });
        }

    }
}
