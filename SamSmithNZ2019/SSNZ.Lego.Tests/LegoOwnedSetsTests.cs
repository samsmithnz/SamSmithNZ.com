using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Lego.DataAccess;
using SSNZ.Lego.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SSNZ.Lego.Tests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class LegoOwnedSetsTests : TestBase
    {
        [TestCategory("IntegrationTest")]
        [TestMethod]
        public async Task GetLegoSetsTest()
        {
            //Arrange
            LegoOwnedSetsDA da = new LegoOwnedSetsDA();

            //Act
            List<LegoOwnedSets> ownedSets = await da.GetLegoOwnedSetsAsync();

            //Assert
            Assert.IsTrue(ownedSets != null);
            Assert.IsTrue(ownedSets.Count > 0);
        }
    }
}

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using AppStack2.Service.Controllers;
//using AppStack2.Data;
//using AppStack2.Models;
//using System.Threading.Tasks;
//using Moq;

//namespace AppStack2.Tests.Service
//{
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    [TestClass]
//    public class DatabasesControllerTest : TestBase
//    {
//        [TestCategory("IntegrationTest")]
//        [TestMethod]
//        public async Task GetDatabasesControllerIntegrationTest()
//        {
//            //Arrange
//            DatabasesController controller = new DatabasesController(new DatabasesDA());

//            //Act
//            List<NewDatabase> databases = await controller.GetDatabases();

//            //Assert
//            Assert.IsTrue(databases != null);
//            Assert.IsTrue(databases.Count > 0);
//        }

//        [TestCategory("MockUnitTest")]
//        [TestMethod]
//        public async Task GetDatabasesControllerMockTest()
//        {
//            //Arrange
//            Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
//            mockRepo.Setup(repo => repo.GetDatabasesAsync()).Returns(Task.FromResult(GetTestDatabases()));
//            DatabasesController controller = new DatabasesController(mockRepo.Object);

//            //Act
//            List<NewDatabase> databases = await controller.GetDatabases();

//            //Assert
//            Assert.IsTrue(databases != null);
//            Assert.IsTrue(databases.Count > 0);
//            TestNewDatabase(databases[0]);
//        }

//        [TestCategory("IntegrationTest")]
//        [TestMethod]
//        public async Task GetDatabasesByServerControllerIntegrationTest()
//        {
//            //Arrange
//            DatabasesController controller = new DatabasesController(new DatabasesDA());
//            int serverCode = 164; //WTRINTRASQL1v

//            //Act
//            List<NewDatabase> databases = await controller.GetDatabasesByServer(serverCode);

//            //Assert
//            Assert.IsTrue(databases != null);
//            Assert.IsTrue(databases.Count > 0);
//        }

//        [TestCategory("MockUnitTest")]
//        [TestMethod]
//        public async Task GetDatabasesByServerControllerMockTest()
//        {
//            //Arrange
//            Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
//            mockRepo.Setup(repo => repo.GetDatabasesByServerAsync(It.IsAny<int>())).Returns(Task.FromResult(GetTestDatabases()));
//            DatabasesController controller = new DatabasesController(mockRepo.Object);
//            int serverCode = 1;

//            //Act
//            List<NewDatabase> databases = await controller.GetDatabasesByServer(serverCode);

//            //Assert
//            Assert.IsTrue(databases != null);
//            Assert.IsTrue(databases.Count > 0);
//            TestNewDatabase(databases[0]);
//        }

//        [TestCategory("IntegrationTest")]
//        [TestMethod]
//        public async Task GetDatabaseControllerIntegrationTest()
//        {
//            //Arrange
//            DatabasesController controller = new DatabasesController(new DatabasesDA());
//            int databaseCode = 1; ; //Appstack

//            //Act
//            NewDatabase database = await controller.GetDatabase(databaseCode);

//            //Assert
//            Assert.IsTrue(database != null);
//        }

//        [TestCategory("MockUnitTest")]
//        [TestMethod]
//        public async Task GetDatabaseControllerMockTest()
//        {
//            //Arrange
//            Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
//            mockRepo.Setup(repo => repo.GetDatabaseAsync(It.IsAny<int>())).Returns(Task.FromResult(GetTestDatabase()));
//            DatabasesController controller = new DatabasesController(mockRepo.Object);
//            int databaseCode = 0;

//            //Act
//            NewDatabase database = await controller.GetDatabase(databaseCode);

//            //Assert
//            Assert.IsTrue(database != null);
//            TestNewDatabase(database);
//        }

//        //[TestCategory("EndToEndTest")]
//        //[TestMethod]
//        //public async Task SaveAndDeleteDatabaseControllerIntegrationTest()
//        //{
//        //    //1.Check to see if the database exists, and delete if it does exist
//        //    //2.Save the new database
//        //    //3.Check that the database was created correctly
//        //    //4.Update the new database
//        //    //5.Check that the database was updated correctly
//        //    //6.Delete the new database
//        //    //7.Check that the database was deleted correctly

//        //    //Arrange base
//        //    DatabasesController controller = new DatabasesController(new DatabasesDA());
//        //    int databaseCode = 1;
//        //    string lastUpdatedBy = "01SSM";

//        //    //1.Check to see if the database exists, and delete if it does exist 
//        //    NewDatabase database = await controller.GetDatabase(databaseCode);
//        //    if (database != null)
//        //    {
//        //        await controller.DeleteDatabase(databaseCode, lastUpdatedBy);
//        //    }

//        //    //2.Save the new database
//        //    NewDatabase newDatabase = new NewDatabase();
//        //    newDatabase.DatabaseName = "TestDatabaseName";
//        //    newDatabase.LastUpdatedBy = lastUpdatedBy;
//        //    database = await controller.SaveDatabase(newDatabase);
//        //    databaseCode = database.DatabaseCode;

//        //    //3.Check that the database was created correctly
//        //    Assert.IsTrue(database != null);
//        //    Assert.IsTrue(database.DatabaseCode > 0);
//        //    Assert.IsTrue(database.DatabaseName == "TestDatabaseName");

//        //    //4.Update the new database
//        //    database.Note = "TestDatabaseNote";
//        //    database = await controller.SaveDatabase(database);

//        //    //5.Check that the database was updated correctly
//        //    Assert.IsTrue(database != null);
//        //    Assert.IsTrue(database.DatabaseCode > 0);
//        //    Assert.IsTrue(database.DatabaseName == "TestDatabaseName");
//        //    Assert.IsTrue(database.Note == "TestDatabaseNote");

//        //    //6.Delete the new database
//        //    bool deleteResult = await controller.DeleteDatabase(databaseCode, lastUpdatedBy);
//        //    Assert.IsTrue(deleteResult);

//        //    //7.Check that the database was deleted correctly
//        //    database = await controller.GetDatabase(databaseCode);
//        //    Assert.IsTrue(database == null);
//        //}

//        //[TestCategory("MockUnitTest")]
//        //[TestMethod]
//        //public async Task SaveDatabaseControllerMockTest()
//        //{
//        //    //Arrange
//        //    Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
//        //    mockRepo.Setup(repo => repo.SaveDatabaseAsync(It.IsAny<NewDatabase>())).Returns(Task.FromResult(GetTestDatabase()));
//        //    DatabasesController controller = new DatabasesController(mockRepo.Object);
//        //    NewDatabase database = new NewDatabase();

//        //    //Act
//        //    NewDatabase databaseResult = await controller.SaveDatabase(database);

//        //    //Assert
//        //    Assert.IsTrue(databaseResult != null);
//        //    TestNewDatabase(databaseResult);
//        //}

//        //[TestCategory("MockUnitTest")]
//        //[TestMethod]
//        //public async Task DeleteDatabaseControllerMockTest()
//        //{
//        //    //Arrange
//        //    Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
//        //    mockRepo.Setup(repo => repo.DeleteDatabaseAsync(It.IsAny<int>(), It.IsAny<string>())).Returns(Task.FromResult(true));
//        //    DatabasesController controller = new DatabasesController(mockRepo.Object);
//        //    int databaseCode = 0;
//        //    string lastUpdatedBy = "";

//        //    //Act
//        //    bool result = await controller.DeleteDatabase(databaseCode, lastUpdatedBy);

//        //    //Assert
//        //    Assert.IsTrue(result == true);
//        //}

//        private void TestNewDatabase(NewDatabase database)
//        {
//            Assert.IsTrue(database.DatabaseCode > 0);
//            Assert.IsTrue(string.IsNullOrEmpty(database.DatabaseName) == false);
//            Assert.IsTrue(string.IsNullOrEmpty(database.Note) == false);
//            Assert.IsTrue(database.IsEncrypted == false);
//            Assert.IsTrue(string.IsNullOrEmpty(database.LastUpdatedBy) == false);
//            Assert.IsTrue(database.LastUpdated > DateTime.MinValue);
//        }

//        private List<NewDatabase> GetTestDatabases()
//        {
//            List<NewDatabase> databases = new List<NewDatabase>
//            {
//                GetTestDatabase()
//            };
//            return databases;
//        }

//        private NewDatabase GetTestDatabase()
//        {
//            return new NewDatabase()
//            {
//                DatabaseCode = 123,
//                DatabaseName = "WTRABCTEStDatabase",
//                Note = "Note",
//                IsEncrypted = false,
//                LastUpdatedBy = "LastUpdatedBy",
//                LastUpdated = DateTime.Now.AddDays(-5)
//            };
//        }

//    }
//}
