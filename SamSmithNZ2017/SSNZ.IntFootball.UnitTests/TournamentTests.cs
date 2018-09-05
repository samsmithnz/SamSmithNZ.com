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
    public class TournamentTests
    {
        [TestMethod]
        public async Task TournamentsExistTest()
        {
            //arrange
            TournamentDataAccess da = new TournamentDataAccess();
            int competitionCode = 1;

            //act
            List<Tournament> results = await da.GetListAsync(competitionCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task TournamentsFirstItemTest()
        {
            TournamentDataAccess da = new TournamentDataAccess();
            int competitionCode = 1;

            //act
            List<Tournament> results = await da.GetListAsync(competitionCode);


            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            bool found19 = false;
            foreach (Tournament item in results)
            {
                if (item.TournamentCode == 19)
                {
                    found19 = true;
                    TestSouthAfricaTournament(item);
                }
            }
            Assert.IsTrue(found19);
        }

        [TestMethod()]
        public async Task TournamentGetSouthAfricaTest()
        {
            TournamentDataAccess da = new TournamentDataAccess();
            int tournamentCode = 19;

            //act
            Tournament result = await da.GetItemAsync(tournamentCode);


            //assert
            Assert.IsTrue(result != null);
            TestSouthAfricaTournament(result);
        }

        private void TestSouthAfricaTournament(Tournament item)
        {
            Assert.IsTrue(item.CoHostFlagName == "");
            Assert.IsTrue(item.CoHostTeamCode == 0);
            Assert.IsTrue(item.CoHostTeamName == null);
            Assert.IsTrue(item.CoHostFlagName2 == "");
            Assert.IsTrue(item.CoHostTeamCode2 == 0);
            Assert.IsTrue(item.CoHostTeamName2 == null);
            Assert.IsTrue(item.CompetitionCode == 1);
            Assert.IsTrue(item.FormatCode == 1);
            Assert.IsTrue(item.GameCount == 64);
            Assert.IsTrue(item.GamesCompleteCount == 64);
            Assert.IsTrue(item.HostFlagName == "22px-Flag_of_South_Africa_svg.png");
            Assert.IsTrue(item.HostTeamCode == 27);
            Assert.IsTrue(item.HostTeamName == "South Africa");
            Assert.IsTrue(item.ImportingGamePercent == 1);
            Assert.IsTrue(item.ImportingGoalsPercent == 1);
            Assert.IsTrue(item.ImportingPenaltyShootoutGoalsPercent == 1);
            Assert.IsTrue(item.ImportingPlayerPercent == 1);
            Assert.IsTrue(item.ImportingTeamPercent == 1);
            Assert.IsTrue(item.ImportingTotalPercentComplete == 1);
            Assert.IsTrue(item.LogoImage == "200px-2010_FIFA_World_Cup_logo_svg.png");
            Assert.IsTrue(item.MaxGameTime > DateTime.MinValue);
            Assert.IsTrue(item.MinGameTime > DateTime.MinValue);
            Assert.IsTrue(item.Notes != "");
            Assert.IsTrue(item.QualificationImage == "305px-2010_world_cup_qualification.png");
            Assert.IsTrue(item.R1FirstGroupCode == "A");
            Assert.IsTrue(item.R1FormatRoundCode == 1);
            Assert.IsTrue(item.R1IsGroupStage == true);
            Assert.IsTrue(item.R1NumberOfGroupsInRound == 8);
            Assert.IsTrue(item.R1NumberOfTeamsFromGroupThatAdvance == 2);
            Assert.IsTrue(item.R1TotalNumberOfTeamsThatAdvance == 16);
            Assert.IsTrue(item.R1NumberOfTeamsInGroup == 4);
            Assert.IsTrue(item.R2FirstGroupCode == "");
            Assert.IsTrue(item.R2FormatRoundCode == 2);
            Assert.IsTrue(item.R2IsGroupStage == false);
            Assert.IsTrue(item.R2NumberOfGroupsInRound == 1);
            Assert.IsTrue(item.R2NumberOfTeamsFromGroupThatAdvance == 0);
            Assert.IsTrue(item.R2TotalNumberOfTeamsThatAdvance == 0);
            Assert.IsTrue(item.R2NumberOfTeamsInGroup == 16);
            Assert.IsTrue(item.R3FirstGroupCode == "");
            Assert.IsTrue(item.R3FormatRoundCode == 0);
            Assert.IsTrue(item.R3IsGroupStage == true);
            Assert.IsTrue(item.R3NumberOfGroupsInRound == 0);
            Assert.IsTrue(item.R3NumberOfTeamsFromGroupThatAdvance == 0);
            Assert.IsTrue(item.R3NumberOfTeamsInGroup == 0);
            Assert.IsTrue(item.R3TotalNumberOfTeamsThatAdvance == 0);
            Assert.IsTrue(item.TournamentCode == 19);
            Assert.IsTrue(item.TournamentName == "South Africa 2010");
            Assert.IsTrue(item.TournamentYear == 2010);
            Assert.IsTrue(item.TotalGoals == 145);
            Assert.IsTrue(item.TotalShootoutGoals == 14);
            Assert.IsTrue(item.TotalPenalties == 9);
        }

        //[TestMethod()]
        //public async Task AlbumsTCATSTest()
        //{
        //    //arrange
        //    AlbumDataAccess da = new AlbumDataAccess();
        //    int albumCode = 14; //The Colour And The Shape

        //    //act
        //    List<Album> results = await da.GetListAsync(true);

        //    //assert
        //    bool foundTCATS = false;
        //    foreach (Album result in results)
        //    {
        //        if (result.AlbumCode == albumCode)
        //        {
        //            TestTCATS(result);
        //            foundTCATS = true;
        //            break;
        //        }
        //    }
        //    Assert.IsTrue(foundTCATS == true);
        //}
    }
}
