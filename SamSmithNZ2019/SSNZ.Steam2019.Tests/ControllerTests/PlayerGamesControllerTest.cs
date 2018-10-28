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
using System.Collections.Generic;

namespace SSNZ.Steam2019.Tests.ControllerTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class PlayerGamesControllerTest
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

        [TestMethod]
        public async Task PlayerGamesSamTest()
        {
            //Arrange
            PlayerGamesController controller = new PlayerGamesController(_redisService);            
            string steamId = "76561197971691578";

            //Act
            List<Game> results = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0].AppID == "15100");
            Assert.IsTrue(results[0].GameName == "Assassin's Creed");
            Assert.IsTrue(results[0].CommunityIsVisible == true);
            Assert.IsTrue(results[0].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
            Assert.IsTrue(results[0].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
            Assert.IsTrue(results[0].TotalMinutesPlayed == 185);
            Assert.IsTrue(results[0].TotalTimeString == "3 hrs");
        }

        [TestMethod]
        public async Task PlayerGamesSamWithoutCacheTest()
        {
            //Arrange
            PlayerGamesController controller = new PlayerGamesController(_redisService);
            string steamId = "76561197971691578";

            //Act
            List<Game> results = await controller.GetPlayer(steamId, false);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            Assert.IsTrue(results[0].AppID == "15100");
            Assert.IsTrue(results[0].GameName == "Assassin's Creed");
            Assert.IsTrue(results[0].CommunityIsVisible == true);
            Assert.IsTrue(results[0].IconURL == "cd8f7a795e34e16449f7ad8d8190dce521967917");
            Assert.IsTrue(results[0].LogoURL == "5450218e6f8ea246272cddcb2ab9a453b0ca7ef5");
            Assert.IsTrue(results[0].TotalMinutesPlayed == 185);
            Assert.IsTrue(results[0].TotalTimeString == "3 hrs");
        }

        [TestMethod]
        public async Task PlayerGamesSamWithNoIconTest()
        {
            //Arrange
            PlayerGamesController controller = new PlayerGamesController(_redisService);
            string steamId = "76561197971691578";
            string appId = "223530";

            //Act
            List<Game> results = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count >= 0);
            foreach (Game item in results)
            {
                if (item.AppID == appId)
                {
                    Assert.IsTrue(item.AppID == "223530");
                    Assert.IsTrue(item.GameName == "Left 4 Dead 2 Beta");
                    Assert.IsTrue(item.CommunityIsVisible == false);
                    Assert.IsTrue(item.IconURL == null);
                    Assert.IsTrue(item.LogoURL == "");
                    Assert.IsTrue(item.TotalMinutesPlayed == 0);
                    Assert.IsTrue(item.TotalTimeString == "0 hrs");
                    break;
                }
            }

        }

        [TestMethod]
        public async Task PlayerGamesRandomWithNoGamesTest()
        {
            //Arrange
            PlayerGamesController controller = new PlayerGamesController(_redisService);
            string steamId = "76561198059077520";

            //Act
            List<Game> results = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);

        }

        [TestMethod]
        public async Task PlayerGamesRandomAnotherWithNoGamesTest()
        {
            //Arrange
            PlayerGamesController controller = new PlayerGamesController(_redisService);
            string steamId = "76561198121979762";

            //Act
            List<Game> results = await controller.GetPlayer(steamId);

            //Assert
            Assert.IsTrue(results != null);
            Assert.IsTrue(results.Count == 0);

        }
        
    }
}