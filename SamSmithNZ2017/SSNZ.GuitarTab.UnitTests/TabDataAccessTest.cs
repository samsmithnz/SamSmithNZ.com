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
    public class TabDataAccessTest
    {

        //1. Tabs exist
        [TestMethod()]
        public async Task TabsExistTest()
        {
            //arrange
            TabDataAccess da = new TabDataAccess();
            short albumCode = 14;

            //act
            List<Tab> results = await da.GetListAsync(albumCode); 

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        //3. Get Tab 14
        [TestMethod()]
        public async Task TabFirstItemTest()
        {
            //arrange
            TabDataAccess da = new TabDataAccess();
            short tabCode = 500;

            //act
            Tab results = await da.GetItemAsync(tabCode); //Tab code of 500/Everlong

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 14);
            Assert.IsTrue(results.Rating == 5);
            Assert.IsTrue(results.TrackCode == 500);
            Assert.IsTrue(results.TrackName == "Everlong");
            Assert.IsTrue(results.TrackOrder == 11);
            Assert.IsTrue(results.TrackText.Length == 7477);
            Assert.IsTrue(results.TuningCode == 2);
            Assert.IsTrue(results.TuningName == "Drop D Tuning");
            Assert.IsTrue(results.LastUpdated > DateTime.MinValue);

        }

        [TestMethod()]
        public async Task TabSaveAndDeleteTest()
        {
            //arrange
            TabDataAccess da = new TabDataAccess();
            int albumCode = 246;
            Tab newTab = new Tab();
            newTab.TrackCode = 0;
            newTab.AlbumCode = albumCode;
            newTab.TrackName = "Test track 14";
            newTab.TrackText = "Test track text 14";

            //act part 1: create the track
            bool result = await da.SaveItemAsync(newTab);

            //assert part 1: check the track was created
            Assert.IsTrue(result);

            //act part 2: get the tracks for the album
            List<Tab> results = await da.GetListAsync(albumCode);

            //assert part 2: check that the track is correct
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TrackCode > 0);
            Assert.IsTrue(results[0].AlbumCode == albumCode);
            Assert.IsTrue(results[0].TrackName == "Test track 14");
            Assert.IsTrue(results[0].TrackText == "Test track text 14");
            Assert.IsTrue(results[0].LastUpdated > DateTime.MinValue);

            //act part 3: delete the tracks
            foreach (Tab item in results)
            {
                await da.DeleteItemAsync(item.TrackCode);
            }

            //act part 4: get the tracks for the album
            results = await da.GetListAsync(albumCode);

            //assert part 4: check that the tracks have all been deleted
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);
        }

    }
}


