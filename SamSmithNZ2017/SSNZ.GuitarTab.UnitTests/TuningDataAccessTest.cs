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
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TuningDataAccessTest
    {
        [TestMethod()]
        public async Task TuningsExistTest()
        {
            //arrange
            TuningDataAccess da = new TuningDataAccess();

            //act
            List<Tuning> results = await da.GetDataAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TuningsFirstItemTest()
        {
            //arrange
            TuningDataAccess da = new TuningDataAccess();

            //act
            List<Tuning> results = await da.GetDataAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TuningCode == 0);
            Assert.IsTrue(results[0].TuningName == "[unknown]");
        }

    }
}


