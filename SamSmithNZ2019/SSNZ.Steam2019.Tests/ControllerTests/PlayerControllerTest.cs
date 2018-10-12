using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSNZ.Steam2019.Service.DataAccess;
using SSNZ.Steam2019.Service.Models;
using System.Threading.Tasks;
using SSNZ.Steam2019.Service.Controllers;
using SSNZ.Steam2019.Service.Services;
using StackExchange.Redis;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace SSNZ.Steam2019.Tests.ControllerTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerControllerTest
    {
        private static RedisService _redisService;

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = config["CacheConnection"];

            var cache = ConnectionMultiplexer.Connect(connectionString);
            var db = cache.GetDatabase();
            _redisService = new RedisService(db);
        }

        //Currently runs in 688ms
        [TestMethod]
        public async Task PlayerControllerSamTest()
        {
            //Arrange
            PlayerController controller = new PlayerController(_redisService);
            string steamId = "76561197971691578";

            //Act
            Player result = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result.SteamID == "76561197971691578");
            Assert.IsTrue(result.PlayerName == "Sam");
            Assert.IsTrue(result.IsPublic == true);
        }


    }
}
