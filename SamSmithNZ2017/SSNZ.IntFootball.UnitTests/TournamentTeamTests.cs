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
    public class TournamentTeamTests
    {
        [TestMethod]
        public async Task TournamentTeamsQualifiedExistTest()
        {
            //arrange
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            int tournamentCode = 19;

            //act
            List<TournamentTeam> results = await da.GetQualifiedTeamsAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TournamentTeamsQualifiedFirstItemTest()
        {
            //Act
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            int tournamentCode = 19;

            //act
            List<TournamentTeam> results = await da.GetQualifiedTeamsAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            bool found1 = false;
            foreach (TournamentTeam item in results)
            {
                if (item.TeamCode == 1)
                {
                    found1 = true;
                    TestNewZealandTeam(item);
                }
            }
            Assert.IsTrue(found1);
        }

        [TestMethod]
        public async Task TournamentTeamsPlacedExistTest()
        {
            //arrange
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            int tournamentCode = 19;

            //act
            List<TournamentTeam> results = await da.GetTeamsPlacingAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TournamentTeamsPlacedFirstItemTest()
        {
            //Act
            TournamentTeamDataAccess da = new TournamentTeamDataAccess();
            int tournamentCode = 19;

            //act
            List<TournamentTeam> results = await da.GetTeamsPlacingAsync(tournamentCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            bool found1 = false;
            foreach (TournamentTeam item in results)
            {
                if (item.TeamCode == 1)
                {
                    found1 = true;
                    TestNewZealandTeam(item);
                }
            }
            Assert.IsTrue(found1);
        }

        private void TestNewZealandTeam(TournamentTeam item)
        {
            Assert.IsTrue(item.TeamCode == 1);
            Assert.IsTrue(item.TeamName == "New Zealand");
            Assert.IsTrue(item.FlagName == "22px-Flag_of_New_Zealand_svg.png");
            Assert.IsTrue(item.CoachName == "Ricki Herbert");
            Assert.IsTrue(item.CoachNationalityFlagName == "22px-Flag_of_New_Zealand_svg.png");
            Assert.IsTrue(item.ELORating >= 0);
            Assert.IsTrue(item.FifaRanking == 0);
            Assert.IsTrue(item.Placing != "");
            Assert.IsTrue(item.RegionCode == 5);
            Assert.IsTrue(item.RegionName == "OFC");
            Assert.IsTrue(item.ELORatingDifference != "");
            Assert.IsTrue(item.IsActive == false);
            Assert.IsTrue(item.ChanceToWin == 0);
            Assert.IsTrue(item.GF >= 0);
            Assert.IsTrue(item.GA >= 0);
            Assert.IsTrue(item.GD >= 0);
        }


    }
}
