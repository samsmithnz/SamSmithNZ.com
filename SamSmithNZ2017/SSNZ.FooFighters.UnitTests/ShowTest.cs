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
    public class ShowTest
    {

        [TestMethod()]
        public async Task ShowsExistForSongTest()
        {
            //arrange
            int songKey = 1;
            ShowDataAccess da = new ShowDataAccess();

            //act
            List<Show> results = await da.GetListBySongAsync(songKey);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ShowsForSongTest()
        {
            //arrange
            int songKey = 1;
            ShowDataAccess da = new ShowDataAccess();

            //act
            List<Show> results = await da.GetListBySongAsync(songKey);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            //Assert.IsTrue(results[0].IsCancelledShow == false);
            //Assert.IsTrue(results[0].IsPostponedShow == false);
            //Assert.IsTrue(results[0].Notes != "");
            //Assert.IsTrue(results[0].NumberOfRecordings >= 0);
            Assert.IsTrue(results[0].NumberOfSongsPlayed >= 0);
            //Assert.IsTrue(results[0].NumberOfUnconfirmedRecordings >= 0);
            //Assert.IsTrue(results[0].OtherPerformers != "");
            Assert.IsTrue(results[0].ShowCity == "Portland, OR");
            //Assert.IsTrue(results[0].ShowCountry == "United States");
            Assert.IsTrue(results[0].ShowDate >= DateTime.MinValue);
            Assert.IsTrue(results[0].ShowCode == 3);
            Assert.IsTrue(results[0].ShowLocation != "");
        }

        [TestMethod()]
        public async Task ShowsExistForYearTest()
        {
            //arrange
            int yearCode = 1995;
            ShowDataAccess da = new ShowDataAccess();

            //act
            List<Show> results = await da.GetListByYearAsync(yearCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task ShowsForYearTest()
        {
            //arrange
            int yearCode = 1995;
            ShowDataAccess da = new ShowDataAccess();

            //act
            List<Show> results = await da.GetListByYearAsync(yearCode);

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            //Assert.IsTrue(results[2].IsCancelledShow == false);
            //Assert.IsTrue(results[2].IsPostponedShow == false);Ok, time to 
            //Assert.IsTrue(results[2].Notes != "");
            //Assert.IsTrue(results[2].NumberOfRecordings >= 0);
            Assert.IsTrue(results[2].NumberOfSongsPlayed >= 0);
            //Assert.IsTrue(results[2].NumberOfUnconfirmedRecordings >= 0);
            //Assert.IsTrue(results[2].OtherPerformers != "");
            Assert.IsTrue(results[2].ShowCity == "Seattle, WA");
            //Assert.IsTrue(results[2].ShowCountry == "United States");
            Assert.IsTrue(results[2].ShowDate >= DateTime.MinValue);
            Assert.IsTrue(results[2].ShowCode == 4);
            Assert.IsTrue(results[2].ShowLocation != "");
        }


        [TestMethod()]
        public async Task Show3Test()
        {
            //arrange
            int showKey = 3;
            ShowDataAccess da = new ShowDataAccess();

            //act
            Show result = await da.GetItemAsync(showKey);

            //assert
            Assert.IsTrue(result != null);
            //Assert.IsTrue(result.IsCancelledShow == false);
            //Assert.IsTrue(result.IsPostponedShow == false);
            //Assert.IsTrue(result.Notes != "");
            //Assert.IsTrue(result.NumberOfRecordings >= 0);
            Assert.IsTrue(result.NumberOfSongsPlayed >= 0);
            //Assert.IsTrue(result.NumberOfUnconfirmedRecordings >= 0);
            //Assert.IsTrue(result.OtherPerformers != "");
            Assert.IsTrue(result.ShowCity == "Portland, OR");
            //Assert.IsTrue(result.ShowCountry == "United States");
            Assert.IsTrue(result.ShowDate >= DateTime.MinValue);
            Assert.IsTrue(result.ShowCode == 3);
            Assert.IsTrue(result.ShowLocation != "");
        }

    }
}