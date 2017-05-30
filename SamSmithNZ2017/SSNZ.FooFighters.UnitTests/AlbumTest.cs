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
    public class AlbumTest
    {

        [TestMethod()]
        public async Task AlbumsExistTest()
        {
            //arrange
            AlbumDataAccess da = new AlbumDataAccess();

            //act
            List<Album> results = await da.GetListAsync();

            //assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod()]
        public async Task AlbumsGetFooFightersTest()
        {
            //arrange
            AlbumDataAccess da = new AlbumDataAccess();
            int albumKey = 1;

            //act
            Album result = await da.GetItemAsync(albumKey);

            //assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.AlbumImage == "220px-FooFighters-FooFighters.jpg");
            Assert.IsTrue(result.AlbumCode == 1);
            Assert.IsTrue(result.AlbumName == "Foo Fighters");
            Assert.IsTrue(result.AlbumReleaseDate >= DateTime.MinValue);
        }

    }
}