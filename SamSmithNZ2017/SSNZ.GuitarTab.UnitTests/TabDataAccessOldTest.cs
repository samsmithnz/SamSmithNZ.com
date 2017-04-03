using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TabDataAccessOldTest
    {

        //1. Tabs exist
        [TestMethod()]
        public void TabsExistOldTest()
        {
            //arrange
            TabDataAccessOld da = new TabDataAccessOld();
            int albumCode = 14;

            //act
            List<Tab> results = da.GetData(albumCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        //3. Get Tab 14
        [TestMethod()]
        public void TabFirstItemOldTest()
        {
            //arrange
            TabDataAccessOld da = new TabDataAccessOld();
            int tabCode = 500;

            //act
            Tab results = da.GetItem(tabCode); //Tab code of 500/Everlong

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 14);
            Assert.IsTrue(results.Rating == 5);
            Assert.IsTrue(results.TabCode == 500);
            Assert.IsTrue(results.TabName == "Everlong");
            Assert.IsTrue(results.TabOrder == 11);
            Assert.IsTrue(results.TabText.Length == 7477);
            Assert.IsTrue(results.TuningCode == 2);
            Assert.IsTrue(results.TuningName == "Drop D Tuning");
            Assert.IsTrue(results.LastUpdated > DateTime.MinValue);
        }

        [TestMethod()]
        public void TabSaveAndDeleteOldTest()
        {
            //arrange
            TabDataAccessOld da = new TabDataAccessOld();
            int albumCode = 246;
            Tab newTab = new Tab();
            newTab.TabCode = 0;
            newTab.AlbumCode = albumCode;
            newTab.TabName = "Test track 14";
            newTab.TabText = "Test track text 14";

            //act part 1: create the track
            bool result = da.SaveItem(newTab);

            //assert part 1: check the track was created
            Assert.IsTrue(result);

            //act part 2: get the tracks for the album
            List<Tab> results = da.GetData(albumCode);

            //assert part 2: check that the track is correct
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TabCode > 0);
            Assert.IsTrue(results[0].AlbumCode == albumCode);
            Assert.IsTrue(results[0].TabName == "Test track 14");
            Assert.IsTrue(results[0].TabText == "Test track text 14");
            Assert.IsTrue(results[0].LastUpdated > DateTime.MinValue);

            //act part 3: delete the tracks
            foreach (Tab item in results)
            {
                da.DeleteItem(item.TabCode);
            }

            //act part 4: get the tracks for the album
            results = da.GetData(albumCode);

            //assert part 4: check that the tracks have all been deleted
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);
        }

    }
}


