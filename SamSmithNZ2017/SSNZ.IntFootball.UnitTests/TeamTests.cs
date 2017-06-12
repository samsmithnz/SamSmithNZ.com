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
    public class TeamTests
    {
        [TestMethod]
        public async Task TeamsExistTest()
        {
            //arrange
            TeamDataAccess da = new TeamDataAccess();

            //act
            List<Team> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TeamsFirstItemTest()
        {
            //Arrange
            TeamDataAccess da = new TeamDataAccess();

            //act
            List<Team> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            bool found1 = false;
            foreach (Team item in results)
            {
                if (item.TeamCode == 1)
                {
                    found1 = true;
                    TestNewZealandTeam(item);
                }
            }
            Assert.IsTrue(found1);
        }

        [TestMethod()]
        public async Task TeamGetNewZealandTest()
        {
            TeamDataAccess da = new TeamDataAccess();
            int TeamCode = 1;

            //act
            Team result = await da.GetItemAsync(TeamCode);


            //assert
            Assert.IsTrue(result != null);
            TestNewZealandTeam(result);
        }

        private void TestNewZealandTeam(Team item)
        {
            Assert.IsTrue(item.TeamCode == 1);
            Assert.IsTrue(item.TeamName == "New Zealand");
            Assert.IsTrue(item.FlagName == "22px-Flag_of_New_Zealand_svg.png");
        }


    }
}
