using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using SamSmithNZ.Service.Models.GuitarTab;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace SamSmithNZ.FunctionalTests
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    public class WebsiteTests
    {
        private ChromeDriver _driver;
        private TestContext _testContextInstance;
        private string _webUrl = null;

        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        [TestMethod]
        public void HomeIndexTest()
        {
            //Arrange
            bool webLoaded;

            //Act
            string serviceURL = _webUrl + "home/index";
            Console.WriteLine(serviceURL);
            _driver.Navigate().GoToUrl(serviceURL);
            webLoaded = (_driver.Url == serviceURL);
            OpenQA.Selenium.IWebElement data = _driver.FindElementByXPath(@"/html/body/pre");

            //Assert
            Assert.IsTrue(webLoaded);
            Assert.IsTrue(data != null);
        }

        [TestInitialize]
        public void SetupTests()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            //_driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
            _driver = new ChromeDriver(chromeOptions);

            if (TestContext.Properties == null || TestContext.Properties.Count == 0)
            {
                _webUrl = "https://https://ssnz-prod-eu-web-staging.azurewebsites.net/";
            }
            else
            {
                _webUrl = TestContext.Properties["WebsiteUrl"].ToString();
            }
        }

        public TestContext TestContext
        {
            get
            {
                return _testContextInstance;
            }
            set
            {
                _testContextInstance = value;
            }
        }

        [TestCleanup()]
        public void CleanupTests()
        {
            _driver.Quit();
        }
    }
}
