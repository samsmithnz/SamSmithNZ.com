using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.GuitarTab.Data;
using SSNZ.GuitarTab.Models;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    public class TrackOrderDataAccessTest
    {
        [TestMethod()]
        public async Task TrackOrderExistTest()
        {
            //arrange
            TrackOrderDataAccess da = new TrackOrderDataAccess();

            //act
            List<TrackOrder> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TrackOrderDataAccess da = new TrackOrderDataAccess();

            //act
            List<TrackOrder> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >=2);
            Assert.IsTrue(results[0].SortOrderCode == 0);
            Assert.IsTrue(results[0].SortOrderName != "[unknown]");
            Assert.IsTrue(results[1].SortOrderCode == 1);
            Assert.IsTrue(results[1].SortOrderName != "");
        }
    }
}
