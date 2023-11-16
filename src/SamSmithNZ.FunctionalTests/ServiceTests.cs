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
    public class ServiceTests
    {
        private ChromeDriver _driver;
        private TestContext _testContextInstance;
        private string _serviceUrl = null;

        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        [TestMethod]
        public void GuitarTabTest()
        {
            //Arrange
            bool serviceLoaded;

            //Act
            string serviceURL = _serviceUrl + "api/guitartab/tuning/gettunings";
            Console.WriteLine(serviceURL);
            _driver.Navigate().GoToUrl(serviceURL);
            serviceLoaded = (_driver.Url == serviceURL);
            OpenQA.Selenium.IWebElement data = _driver.FindElementByXPath(@"/html/body/pre");

            //Assert
            Assert.IsTrue(serviceLoaded);
            Assert.IsTrue(data != null);
            //Convert the JSON to the owners model
            IEnumerable<Tuning> owners = JsonConvert.DeserializeObject<IEnumerable<Tuning>>(data.Text);
            Assert.IsTrue(owners.Count() > 0); //There is more than one owner
            Assert.IsTrue(owners.FirstOrDefault().TuningCode == 0); //The first item has an id
            Assert.IsTrue(owners.FirstOrDefault().TuningName == "[unknown]"); //The first item has an name
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
                _serviceUrl = "https://ssnz-prod-eu-service-staging.azurewebsites.net/";
            }
            else
            {
                _serviceUrl = TestContext.Properties["ServiceUrl"].ToString();
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
