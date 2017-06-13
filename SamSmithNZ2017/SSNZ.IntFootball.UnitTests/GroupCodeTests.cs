using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.IntFootball.Data;
using SSNZ.IntFootball.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SSNZ.IntFootball.UnitTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class GroupCodeTests
    {
        [TestMethod]
        public async Task GroupCodesExistTest()
        {
            //arrange
            GroupCodeDataAccess da = new GroupCodeDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;

            //act
            List<GroupCode> results = await da.GetListAsync(tournamentCode, roundNumber);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task GroupCodesFirstItemTest()
        {
            //arrange
            GroupCodeDataAccess da = new GroupCodeDataAccess();
            int tournamentCode = 19;
            int roundNumber = 1;

            //act
            List<GroupCode> results = await da.GetListAsync(tournamentCode, roundNumber);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].RoundCode == "A");
        }

    }
}
