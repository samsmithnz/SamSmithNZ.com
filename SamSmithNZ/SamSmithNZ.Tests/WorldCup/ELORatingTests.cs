using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.WorldCup;
using SamSmithNZ.Service.DataAccess.WorldCup;
using SamSmithNZ.Service.Models.WorldCup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.WorldCup
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class ELORatingTests : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task EvenTeamsELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1000;
            int team2ELORating = 1000;
            bool team1Won = true;
            bool team2Won = false;

            //act
            (int,int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating, 
                team1Won, team2Won);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1016, result.Item1);
            Assert.AreEqual(984, result.Item2);
        }

        [TestMethod()]
        public async Task GermanyJapan2022ELORatingsTest()
        {
            //arrange
            EloRating eloRating = new();
            int team1ELORating = 1963;
            int team2ELORating = 1798;
            bool team1Won = false;
            bool team2Won = true;

            //act
            (int,int) result = eloRating.GetEloRatingScoresForMatchUp(team1ELORating, team2ELORating, 
                team1Won, team2Won);

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1891, result.Item1);
            Assert.AreEqual(1870, result.Item2);
        }
        

    }
}
