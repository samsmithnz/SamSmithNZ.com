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
    public class RatingDataAccessTest
    {
        [TestMethod()]
        public async Task RatingsExistTest()
        {
            //arrange
            RatingDataAccess da = new RatingDataAccess();

            //act
            List<Rating> results = await da.GetDataAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task RatingsFirstItemTest()
        {
            //arrange
            System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
            RatingDataAccess da = new RatingDataAccess();

            //act
            List<Rating> results = await da.GetDataAsync();
            
            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].RatingCode == 0);
            Assert.IsTrue(results[1].RatingCode == 1);
            sw.Stop();
            System.Diagnostics.Debug.WriteLine("Async time taken: {0}ms", sw.Elapsed.TotalMilliseconds);
        }

    }
}