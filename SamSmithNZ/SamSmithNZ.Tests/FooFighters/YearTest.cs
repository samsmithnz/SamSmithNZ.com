using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class YearTest:BaseIntegrationTest
    {

        [TestMethod()]
        public async Task YearsExistTest()
        {
            //arrange
            YearController controller = new(new YearDataAccess(base.Configuration));

            //act
            List<Year> items = await controller.GetYears();

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);

        }

        [TestMethod()]
        public async Task YearsTest()
        {
            //arrange
            YearController controller = new(new YearDataAccess(base.Configuration));

            //act
            List<Year> items = await controller.GetYears();

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0);
            Assert.IsTrue(items[^1].YearCode == 1995);  //^1 is same as items.Count-1
            Assert.IsTrue(items[^1].YearText != ""); //^1 is same as items.Count-1
        }

    }
}