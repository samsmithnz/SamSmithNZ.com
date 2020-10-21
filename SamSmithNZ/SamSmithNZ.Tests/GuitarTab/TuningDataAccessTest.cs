using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TuningDataAccessTest : BaseIntegrationTest
    {
        [TestMethod()]
        public async Task TuningsExistTest()
        {
            //arrange
            TuningController controller = new TuningController(new TuningDataAccess(base.Configuration));

            //act
            List<Tuning> results = await controller.GetTunings();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TuningController controller = new TuningController(new TuningDataAccess(base.Configuration));

            //act
            List<Tuning> results = await controller.GetTunings();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TuningCode == 0);
            Assert.IsTrue(results[0].TuningName == "[unknown]");
        }

    }
}


