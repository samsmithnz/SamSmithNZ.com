using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamSmithnNZ.Tests;
using SamSmithNZ.Service.Controllers.FooFighters;
using SamSmithNZ.Service.DataAccess.FooFighters;
using SamSmithNZ.Service.Models.FooFighters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SamSmithNZ.Tests.FooFighters
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class AlbumTest : BaseIntegrationTest
    {

        [TestMethod()]
        public async Task AlbumsExistTest()
        {
            //arrange
            AlbumController controller = new AlbumController(new AlbumDataAccess(base.Configuration));

            //act
            List<Album> items = await controller.GetAlbums(); 

            //assert
            Assert.IsTrue(items != null);
            Assert.IsTrue(items.Count > 0); //There is more than one
            Assert.IsTrue(items[0].AlbumCode > 0); //The first item has an id
            Assert.IsTrue(items[0].AlbumName.Length > 0); //The first item has an name
        }

        [TestMethod()]
        public async Task AlbumsGetFooFightersTest()
        {
            //arrange
            AlbumController controller = new AlbumController(new AlbumDataAccess(base.Configuration));
            int albumKey = 1;

            //act
            Album item = await controller.GetAlbum(albumKey);

            //assert
            Assert.IsTrue(item != null);
            Assert.IsTrue(item.AlbumImage == "220px-FooFighters-FooFighters.jpg");
            Assert.IsTrue(item.AlbumCode == 1);
            Assert.IsTrue(item.AlbumName == "Foo Fighters");
            Assert.IsTrue(item.AlbumReleaseDate >= DateTime.MinValue);
        }

    }
}