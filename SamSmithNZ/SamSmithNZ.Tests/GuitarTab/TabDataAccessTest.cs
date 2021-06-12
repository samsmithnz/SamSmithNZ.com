using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.GuitarTab;
using SamSmithNZ.Service.DataAccess.GuitarTab;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.GuitarTab
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class TabDataAccessTest : BaseIntegrationTest
    {

        //1. Tabs exist
        [TestMethod()]
        public async Task TabsExistTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 0; //order by track order

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder); 

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        //3. Get tab 500/Everlong
        [TestMethod()]
        public async Task TabFirstItemTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int tabCode = 500;

            //act
            Tab results = await controller.GetTab(tabCode); //Tab code of 500/Everlong

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.AlbumCode == 14);
            Assert.IsTrue(results.Rating == 5);
            Assert.IsTrue(results.TabCode == 500);
            Assert.IsTrue(results.TabName == "Everlong");
            Assert.IsTrue(results.TabNameTrimed == "Everlong");
            Assert.IsTrue(results.TabOrder == 11);
            Assert.IsTrue(results.TabText.Length == 7477);
            Assert.IsTrue(results.TuningCode == 2);
            Assert.IsTrue(results.TuningName == "Drop D Tuning");
            Assert.IsTrue(results.LastUpdated > DateTime.MinValue);
        }

        //3. Get tab 0/null
        [TestMethod()]
        public async Task Tab0ItemTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int tabCode = 0;

            //act
            Tab results = await controller.GetTab(tabCode); //Tab code of 0/nothing

            //assert
            Assert.IsTrue(results == null);
        }

        //Test album 14, sorted by track
        [TestMethod()]
        public async Task TabAlbumSortedbyTrackOrderTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 0; //order by track order

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder); 

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 13);
            int i = 0;
            foreach (Tab result in results)
            {
                i++;
                if (result.TabCode == 500)
                {
                    Assert.IsTrue(i == 11);
                    Assert.IsTrue(result.AlbumCode == 14);
                    Assert.IsTrue(result.Rating == 5);
                    Assert.IsTrue(result.TabCode == 500);
                    Assert.IsTrue(result.TabName == "Everlong");
                    Assert.IsTrue(result.TabNameTrimed == "Everlong");
                    Assert.IsTrue(result.TabOrder == 11);
                    Assert.IsTrue(result.TabText.Length == 7477);
                    Assert.IsTrue(result.TuningCode == 2);
                    Assert.IsTrue(result.TuningName == "Drop D Tuning");
                    Assert.IsTrue(result.LastUpdated > DateTime.MinValue);
                    break;
                }
            }

        }

        //Test album 14
        [TestMethod()]
        public async Task TabAlbumSortedbyTuningTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 14;
            int sortOrder = 1; //order by tuning

            //act
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder);

            //assert
            int i = 0;
            foreach (Tab result in results)
            {
                i++;
                if (result.TabCode == 500)
                {
                    Assert.IsTrue(i == 5); //Is 5th in the tab order when sorting by tuning
                    Assert.IsTrue(result.AlbumCode == 14);
                    Assert.IsTrue(result.Rating == 5);
                    Assert.IsTrue(result.TabCode == 500);
                    Assert.IsTrue(result.TabName == "Everlong");
                    Assert.IsTrue(result.TabNameTrimed == "Everlong");
                    Assert.IsTrue(result.TabOrder == 11);
                    Assert.IsTrue(result.TabText.Length == 7477);
                    Assert.IsTrue(result.TuningCode == 2);
                    Assert.IsTrue(result.TuningName == "Drop D Tuning");
                    Assert.IsTrue(result.LastUpdated > DateTime.MinValue);
                    break;
                }
            }

        }

        [TestMethod()]
        public async Task TabSaveAndDeleteTest()
        {
            //arrange
            TabController controller = new(new TabDataAccess(base.Configuration));
            int albumCode = 246;
            int sortOrder = 0;
            Tab newTab = new();
            newTab.TabCode = 0;
            newTab.AlbumCode = albumCode;
            newTab.TabName = "Test track 14";
            newTab.TabText = "Test track text 14";

            //act part 1: create the track
            bool result = await controller.SaveTab(newTab);

            //assert part 1: check the track was created
            Assert.IsTrue(result);

            //act part 2: get the tracks for the album
            List<Tab> results = await controller.GetTabs(albumCode, sortOrder);

            //assert part 2: check that the track is correct
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
            Assert.IsTrue(results[0].TabCode > 0);
            Assert.IsTrue(results[0].AlbumCode == albumCode);
            Assert.IsTrue(results[0].TabName == "Test track 14");
            Assert.IsTrue(results[0].TabNameTrimed == "Testtrack14");
            Assert.IsTrue(results[0].TabText == "Test track text 14");
            Assert.IsTrue(results[0].LastUpdated > DateTime.MinValue);

            //act part 3: delete the tracks
            foreach (Tab item in results)
            {
                await controller.DeleteTab(item.TabCode);
            }

            //act part 4: get the tracks for the album
            results = await controller.GetTabs(albumCode, sortOrder);

            //assert part 4: check that the tracks have all been deleted
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);
        }

    }
}


