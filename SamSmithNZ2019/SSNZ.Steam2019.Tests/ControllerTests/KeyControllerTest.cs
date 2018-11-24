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
using Moq;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace SSNZ.Steam2019.Tests.ControllerTests
{
    [TestClass]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public class KeyControllerTest
    {
        private static IConfiguration _configuration;
        private static KeyVaultClient _keyVaultClient;

        [ClassInitialize]
        public static void InitTestSuite(TestContext testContext)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            _configuration = builder.Build();

            AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
            KeyVaultClient _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            builder.AddAzureKeyVault(@"https://ssnzkeyvault.vault.azure.net/", _keyVaultClient, new DefaultKeyVaultSecretManager());

            _configuration = builder.Build();
        }

        //https://stackoverflow.com/questions/48064617/cant-get-azure-keyvault-configuration-provider-to-work
        //Currently runs in 688ms
        [TestMethod]
        public void KeyControllerSecretTest()
        {
            //Arrange
            KeyController controller = new KeyController(_configuration);

            //Act
            //string result = controller.GetData();
            string result = _keyVaultClient.GetSecretAsync("CacheConnection").Result.Value;

            //Assert
            Assert.IsTrue(result != null);
            //Assert.IsTrue(result == "Secret");
            //Assert.IsTrue(result.PlayerName == "Sam");
            //Assert.IsTrue(result.IsPublic == true);
        }

        [TestMethod]
        public void KeyControllerSecretMockTest()
        {
            //Arrange
            //Mock<IDatabasesDA> mockRepo = new Mock<IDatabasesDA>();
            //mockRepo.Setup(repo => repo.GetDatabasesAsync()).Returns(Task.FromResult("Secret"));
            //KeyController controller = new KeyController(mockRepo.Object);

            Mock<IConfigurationRoot> mockRepo = new Mock<IConfigurationRoot>();
            mockRepo.SetupGet(x => x[It.IsAny<string>()]).Returns("Secret");
            KeyController controller = new KeyController(mockRepo.Object);

            //Act
            string result = controller.GetData();

            //Assert
            Assert.IsTrue(result != null);
            Assert.IsTrue(result == "Secret");
            //Assert.IsTrue(result.PlayerName == "Sam");
            //Assert.IsTrue(result.IsPublic == true);
        }

        private string GetKeyResult()
        {
            return "Secret";
        }

    }
}
