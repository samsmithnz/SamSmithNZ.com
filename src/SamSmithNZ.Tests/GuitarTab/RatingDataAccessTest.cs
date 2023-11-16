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
    public class RatingDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task RatingsExistTest()
        {
            //arrange
            RatingController controller = new(new RatingDataAccess(base.Configuration));

            //act
            List<Rating> results = await controller.GetRatings();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task RatingsFirstItemTest()
        {
            //arrange
            RatingController controller = new(new RatingDataAccess(base.Configuration));

            //act
            List<Rating> results = await controller.GetRatings();
            
            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].RatingCode == 0);
            Assert.IsTrue(results[1].RatingCode == 1);
        }

    }
}