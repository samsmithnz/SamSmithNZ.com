//using System;
//using System.Text;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using SamSmithNZ2015.Controllers;
//using System.Web.Mvc;
//using SamSmithNZ2015.Models.GuitarTab;
//using SamSmithNZ2015.Core.GuitarTab;

//namespace SSNZ.GuitarTab.UnitTests
//{
//    [TestClass]
//    public class GuitarTabControllerTest
//    {

//        [TestMethod()]
//        public void AlbumListControllerTest()
//        {
//            //arrange
//            GuitarTabController controller = new GuitarTabController();

//            //act
//            var result = controller.Index() as ActionResult;

//            //assert
//            Assert.IsTrue(((System.Web.Mvc.ViewResultBase)(result)).Model.GetType() == typeof(ArtistAlbumViewModel));
//            ArtistAlbumViewModel model = ((System.Web.Mvc.ViewResultBase)(result)).Model as ArtistAlbumViewModel;

//            Assert.IsTrue(model.Albums[1].ArtistName == "Ash");
//            Assert.IsTrue(model.Albums[1].AlbumName == "Trailer");
//            Assert.IsTrue(model.Artists[1].ArtistName == "Audioslave");
//        }

//        [TestMethod()]
//        public void AlbumTabListControllerTest()
//        {
//            //arrange
//            GuitarTabController controller = new GuitarTabController();

//            //act
//            var result = controller.AlbumTabList(14, true) as ActionResult;

//            //assert
//            Assert.IsTrue(((System.Web.Mvc.ViewResultBase)(result)).Model.GetType() == typeof(AlbumTabsViewModel));
//            AlbumTabsViewModel model = ((System.Web.Mvc.ViewResultBase)(result)).Model as AlbumTabsViewModel;

//            Assert.IsTrue(model.Album.AlbumCode == 14);
//            Assert.IsTrue(model.Album.AlbumYear == 1997);
//            Assert.IsTrue(model.Tabs[0].AlbumCode == 14);
//            Assert.IsTrue(model.Tabs[0].TrackName == "Doll");
//            Assert.IsTrue(model.Tabs[10].TrackName == "Everlong");
//        }

//        [TestMethod()]
//        public void SearchResultsControllerTest()
//        {
//            //arrange
//            GuitarTabController controller = new GuitarTabController();

//            //act
//            var result = controller.SearchResults("Everlong") as ActionResult;

//            //assert
//            Assert.IsTrue(((System.Web.Mvc.ViewResultBase)(result)).Model.GetType() == typeof(List<Search>));
//            List<Search> model = ((System.Web.Mvc.ViewResultBase)(result)).Model as List<Search>;

//            Assert.IsTrue(model[0].AlbumCode == 14);
//            Assert.IsTrue(model[0].SearchText == "Everlong");
//            Assert.IsTrue(model[0].TrackName == "Everlong");
//            Assert.IsTrue(model[0].TrackResult == "11. Everlong");

//        }

//        [TestMethod()]
//        public void ArtistControllerTest()
//        {
//            //arrange
//            GuitarTabController controller = new GuitarTabController();

//            //act
//            var result = controller.ArtistSideBar() as ActionResult;

//            //assert
//            Assert.IsTrue(((System.Web.Mvc.ViewResultBase)(result)).Model.GetType() == typeof(List<Artist>));
//            IList<Artist> model = ((System.Web.Mvc.ViewResultBase)(result)).Model as List<Artist>;

//            Assert.IsTrue(model[1].ArtistName == "Audioslave");
//        }

//    }
//}


