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
    public class TabDataAccessOldTest
    {

        //1. Tabs exist
        [TestMethod()]
        public void TabsExistOldTest()
        {
            //arrange
            TabDataAccessOld da = new TabDataAccessOld();
            short albumCode = 14;

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
            short tabCode = 500;

            //act
            Tab results = da.GetItem(tabCode); //Tab code of 500/Everlong

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 14);
            Assert.IsTrue(results.Rating == 5);
            Assert.IsTrue(results.TrackCode == 500);
            Assert.IsTrue(results.TrackName == "Everlong");
            Assert.IsTrue(results.TrackOrder == 11);
            Assert.IsTrue(results.TrackText.Length == 7477);
            Assert.IsTrue(results.TuningCode == 2);
        }

    }
}


