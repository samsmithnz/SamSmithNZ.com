using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using SamSmithNZ2015.Core.GuitarTab;
//using SamSmithNZ2015.Models.GuitarTab;

namespace SamSmithNZ2017.Controllers
{
    public class GuitarTabController : Controller
    {
        //
        // GET: /GuitarTab/

        public ActionResult Index()
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //ViewBag.Message = "Welcome to my Guitar Tab application";

            //bool isGuitarTabAdmin = false;
            //if (Request == null || User == null)
            //{
            //    isGuitarTabAdmin = true;
            //}
            //else
            //{
            //    isGuitarTabAdmin = User.Identity.IsAuthenticated && (User.IsInRole(SamSmithNZ2015.Models.AccountConstants.ACCOUNT_WebAdmin) == true);
            //}

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.ArtistDataAccess t = new SamSmithNZ2015.Core.GuitarTab.DataAccess.ArtistDataAccess();
            //IList<Artist> Artistlist = t.GetItems(1);

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess r = new SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess();
            //IList<Album> Albumlist = r.GetItems(isGuitarTabAdmin);

            //// Return view
            //return View(new ArtistAlbumViewModel(Artistlist, Albumlist));
        }

        public ActionResult AlbumTabList(short albumcode, bool isAdmin)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //ViewBag.Message = "Welcome to albumcode: " + albumcode.ToString();

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess t = new SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess();
            //Album album = t.GetItem(albumcode, isAdmin);

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess r = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //IList<Tab> tabList = r.GetItems(albumcode);

            //return View(new AlbumTabsViewModel(album, tabList));
        }

        //[HttpPost]
        public ActionResult SearchResults(string searchText)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //ViewBag.Message = "Searching: " + searchText;

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.SearchDataAccess r = new SamSmithNZ2015.Core.GuitarTab.DataAccess.SearchDataAccess();
            ////Pass in the search parameters and get a record id
            //Guid recordID = r.Commit(searchText);
            ////using the record ID, get the results 
            //IList<Search> list = r.GetItems(recordID);

            //return View(list);
        }


        //This creates the Artist side bar on the left hand side
        [ChildActionOnly]
        public ActionResult ArtistSideBar()
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.ArtistDataAccess t = new SamSmithNZ2015.Core.GuitarTab.DataAccess.ArtistDataAccess();
            //IList<Artist> Artistlist = t.GetItems(1);

            //// Return partial view
            //return PartialView(Artistlist);
        }

        [Authorize(Roles = "WebAdmin")]
        public ActionResult AddEditAlbum(int albumcode)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //bool isAdmin = User.Identity.IsAuthenticated && (User.IsInRole(SamSmithNZ2015.Models.AccountConstants.ACCOUNT_WebAdmin) == true);

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess da = new Core.GuitarTab.DataAccess.AlbumDataAccess();
            //SamSmithNZ2015.Core.GuitarTab.Album album = da.GetItem(albumcode, isAdmin);

            //if (album == null)
            //{
            //    album = new SamSmithNZ2015.Core.GuitarTab.Album();
            //    album.IsNewAlbum = true;
            //    album.IncludeInIndex = true;
            //    album.IncludeOnWebsite = true;
            //}

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess da2 = new Core.GuitarTab.DataAccess.TabDataAccess();
            //List<Tab> tabs = da2.GetItems((short)albumcode);

            //return View(new AlbumTabsViewModel(album, tabs));
        }


        [Authorize(Roles = "WebAdmin")]
        public ActionResult AddEditTrack(short trackCode)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess da = new Core.GuitarTab.DataAccess.TabDataAccess();
            //SamSmithNZ2015.Core.GuitarTab.Tab tab = da.GetItem(trackCode);

            //if (tab == null)
            //{
            //    tab = new SamSmithNZ2015.Core.GuitarTab.Tab();
            //}

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TuningDataAccess da2 = new Core.GuitarTab.DataAccess.TuningDataAccess();
            //List<Tuning> tunings = da2.GetItems();

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.RatingDataAccess da3 = new Core.GuitarTab.DataAccess.RatingDataAccess();
            //List<Rating> ratings = da3.GetItems();

            //return View(new TabViewModel(tab, tunings, ratings));
        }

        [Authorize(Roles = "WebAdmin")]
        [HttpPost]
        public ActionResult SaveAlbum(short albumCode, string txtArtist, string txtAlbumName, string txtYear,
            bool chkIsBassTab, bool chkIncludeInIndex, bool chkIncludeOnWebsite, bool chkIsMiscCollectionAlbum, string txtTrackList)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            ////parse and convert the numbers
            //short year = 0;
            //if (string.IsNullOrEmpty(txtYear) == true || short.TryParse(txtYear.ToString(), out year) == false)
            //{
            //    year = 0;
            //}
            //if (string.IsNullOrEmpty(txtTrackList) == true)
            //{
            //    txtTrackList = "";
            //}
            //string[] tracks = txtTrackList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            ////setup the album
            //Album a = new Album();
            //a.AlbumCode = albumCode;
            //a.ArtistName = txtArtist;
            //a.AlbumName = txtAlbumName;
            //a.AlbumYear = year;
            //a.IsBassTab = chkIsBassTab;
            //a.IsMiscCollectionAlbum = chkIsMiscCollectionAlbum;
            //a.IncludeOnWebsite = chkIncludeOnWebsite;
            //a.IncludeInIndex = chkIncludeInIndex;
            //a.IsNewAlbum = (albumCode == 0);

            ////save the album
            //SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess da = new SamSmithNZ2015.Core.GuitarTab.DataAccess.AlbumDataAccess();
            //da.Save(a);

            ////Save all of the tracks on the album
            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess da2 = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //short i = 1;
            //foreach (string item in tracks)
            //{
            //    Tab t = new Tab();
            //    t.AlbumCode = (short)a.AlbumCode;
            //    t.Rating = 0;
            //    t.TuningCode = 0;
            //    t.TrackName = item;
            //    t.TrackText = "";
            //    t.TrackOrder = i;
            //    da2.Save(t);
            //    i++;
            //}

            ////redirect to the home page
            //return RedirectToAction("Index", "GuitarTab");
        }

        public ActionResult AddNewTrack(short albumCode)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess r = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //IList<Tab> tabList = r.GetItems(albumCode);
            //int lastOrder = 0;
            //foreach (Tab item in tabList)
            //{
            //    if (item.TrackOrder > lastOrder)
            //    {
            //        lastOrder = item.TrackOrder;
            //    }
            //}

            ////setup the tab
            //Tab t = new Tab();
            //t.AlbumCode = albumCode;
            //t.TrackCode = 0;
            //t.TrackName = "New Track";
            //t.TrackOrder = (short)(lastOrder + 1);
            //t.TrackText = "";
            //t.TuningCode = 0;
            //t.Rating = 0;

            ////save the tab
            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess da = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //da.Save(t);

            //return RedirectToAction("AddEditAlbum", "GuitarTab", new { @albumCode = albumCode });
        }

        public ActionResult DeleteTrack(short albumCode, short trackCode)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess r = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //r.Delete(trackCode);

            //IList<Tab> tabList = r.GetItems(albumCode);
            //int newOrder = 1;
            //foreach (Tab item in tabList)
            //{
            //    item.TrackOrder = (short)newOrder;
            //    r.Save(item);
            //    newOrder++;
            //}

            //return RedirectToAction("AddEditAlbum", "GuitarTab", new { @albumCode = albumCode });
        }



        [Authorize(Roles = "WebAdmin")]
        [HttpPost]
        public ActionResult SaveTrack(short albumCode, short trackCode, string txtTrackName, short txtOrder, string txtTrackText, string cboTuning, string cboRating)
        {
            return Redirect("http://samsmithnz.com/GuitarTab/Index");

            ////Parse and convert the numbers
            //short order = 0;
            //if (short.TryParse(txtOrder.ToString(), out order) == false)
            //{
            //    order = 0;
            //}
            //short tuning = 0;
            //if (cboTuning == null || short.TryParse(cboTuning.ToString(), out tuning) == false)
            //{
            //    tuning = 0;
            //}
            //short rating = 0;
            //if (cboRating == null || short.TryParse(cboRating.ToString(), out rating) == false)
            //{
            //    rating = 0;
            //}

            ////setup the tab
            //Tab t = new Tab();
            //t.AlbumCode = albumCode;
            //t.TrackCode = trackCode;
            //t.TrackName = txtTrackName;
            //t.TrackOrder = order;
            //t.TrackText = txtTrackText;//.Replace("\r\n", Environment.NewLine);
            //t.TuningCode = tuning;
            //t.Rating = rating;

            ////save the tab
            //SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess da = new SamSmithNZ2015.Core.GuitarTab.DataAccess.TabDataAccess();
            //da.Save(t);

            ////redirect back to the album we just edited
            //return RedirectToAction("AlbumTabList", "GuitarTab", new { albumCode = t.AlbumCode, isAdmin = true });
        }
    }
}
