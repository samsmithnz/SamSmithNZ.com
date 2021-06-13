using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TrackOrderDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TrackOrderExistTest()
        {
            //arrange
            TrackOrderController controller = new(new TrackOrderDataAccess(base.Configuration));

            //act
            List<TrackOrder> results = await controller.GetTrackOrders();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TrackOrderController controller = new(new TrackOrderDataAccess(base.Configuration));

            //act
            List<TrackOrder> results = await controller.GetTrackOrders();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 2);
            Assert.IsTrue(results[0].SortOrderCode == 0);
            Assert.IsTrue(results[0].SortOrderName != "[unknown]");
            Assert.IsTrue(results[1].SortOrderCode == 1);
            Assert.IsTrue(results[1].SortOrderName != "");
        }
    }
}
