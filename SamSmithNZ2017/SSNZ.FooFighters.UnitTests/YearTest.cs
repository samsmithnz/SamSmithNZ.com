using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.FooFighters.Data;
using SSNZ.FooFighters.Models;
using System.Threading.Tasks;

namespace SSNZ.GuitarTab.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class YearTest
    {

        [TestMethod()]
        public async Task YearsExistTest()
        {
            //arrange
            ShowYearDataAccess da = new ShowYearDataAccess();

            //act
            List<ShowYear> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task YearsTest()
        {
            //arrange
            ShowYearDataAccess da = new ShowYearDataAccess();

            //act
            List<ShowYear> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[results.Count - 1].YearCode == 1995);
            Assert.IsTrue(results[results.Count - 1].YearText != "");
        }

    }
}