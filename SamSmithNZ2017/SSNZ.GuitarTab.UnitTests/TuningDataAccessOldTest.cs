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
    public class TuningDataAccessOldTest
    {
        [TestMethod()]
        public void TuningsExistOldTest()
        {
            //arrange
            TuningDataAccessOld da = new TuningDataAccessOld();

            //act
            List<Tuning> results = da.GetData();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public void TuningsFirstItemOldTest()
        {
            //arrange
            TuningDataAccessOld da = new TuningDataAccessOld();

            //act
            List<Tuning> results = da.GetData();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TuningCode == 0);
            Assert.IsTrue(results[0].TuningName == "[unknown]");
        }

    }
}


