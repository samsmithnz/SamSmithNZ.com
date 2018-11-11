using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace SamSmithNZ2018.UITests
{
    [TestClass]
    public class SSNZWebTest
    {
        public TestContext TestContext { get; set; }
        private string _SSNZWebUrl = null;
        private string _SSNZSteamServiceUrl = null;
        private string _SSNZFFServiceUrl = null;
        private string _SSNZIntFootballServiceUrl = null;
        private string _SSNZITunesServiceUrl = null;
        private string _SSNZGuitarTabServiceUrl = null;
        private string _SSNZMAndMServiceUrl = null;

        [TestInitialize]
        public void SetupTests()
        {
            if (TestContext.Properties == null || TestContext.Properties.Count == 0)
            {
                _SSNZWebUrl = "https://www.samsmithnz.com/";
                _SSNZSteamServiceUrl = "https://ssnzsteam2019webservice.azurewebsites.net/";
                _SSNZFFServiceUrl = "https://ssnzfoofighterswebservice.azurewebsites.net/";
                _SSNZIntFootballServiceUrl = "https://ssnzintfootballwebservice.azurewebsites.net/";
                _SSNZITunesServiceUrl = "https://ssnzituneswebservice.azurewebsites.net/";
                _SSNZGuitarTabServiceUrl = "https://ssnzguitartabservice.azurewebsites.net/";
                _SSNZMAndMServiceUrl = "https://ssnzmandmcounterservice.azurewebsites.net/";
            }
            else
            {
                _SSNZWebUrl = TestContext.Properties["SSNZWebUrl"].ToString();
                _SSNZSteamServiceUrl = TestContext.Properties["SSNZSteamServiceUrl"].ToString();
                _SSNZFFServiceUrl = TestContext.Properties["SSNZFFServiceUrl"].ToString();
                _SSNZIntFootballServiceUrl = TestContext.Properties["SSNZIntFootballServiceUrl"].ToString();
                _SSNZITunesServiceUrl = TestContext.Properties["SSNZITunesServiceUrl"].ToString();
                _SSNZGuitarTabServiceUrl = TestContext.Properties["SSNZGuitarTabServiceUrl"].ToString();
                _SSNZMAndMServiceUrl = TestContext.Properties["SSNZMAndMServiceUrl"].ToString();
            }
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZHomePage()
        {
            //Arrange
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            bool ssnzWebLoaded = false;
            bool ssnzSteamServiceLoaded = false;
            bool ssnzFFServiceLoaded = false;
            bool ssnzIntFootballServiceLoaded = false;
            bool ssnzITunesServiceLoaded = false;
            bool ssnzGuitarTabServiceLoaded = false;
            bool ssnzMAndMServiceLoaded = false;

            //Act
            using (ChromeDriver browser = new ChromeDriver(chromeOptions))
            {

                browser.Navigate().GoToUrl(this._SSNZWebUrl);
                ssnzWebLoaded = (browser.Url == this._SSNZWebUrl);
                //Assert.AreEqual("AppStack", Driver.FindElement(By.ClassName("navbar-brand")).Text);

                string steamURL = this._SSNZSteamServiceUrl + "api/Player/GetPlayer?steamId=76561197971691578";
                browser.Navigate().GoToUrl(steamURL);
                ssnzSteamServiceLoaded = (browser.Url == steamURL);

                string ffURL = this._SSNZFFServiceUrl + "";
                browser.Navigate().GoToUrl(ffURL);
                ssnzFFServiceLoaded = (browser.Url == ffURL);

                string intFootballURL = this._SSNZIntFootballServiceUrl + "";
                browser.Navigate().GoToUrl(intFootballURL);
                ssnzIntFootballServiceLoaded = (browser.Url == intFootballURL);

                string itunesURL = this._SSNZITunesServiceUrl + "";
                browser.Navigate().GoToUrl(itunesURL);
                ssnzITunesServiceLoaded = (browser.Url == itunesURL);

                string guitartabURL = this._SSNZGuitarTabServiceUrl + "";
                browser.Navigate().GoToUrl(guitartabURL);
                ssnzGuitarTabServiceLoaded = (browser.Url == guitartabURL);

                //string mandmURL = this._SSNZMAndMServiceUrl + "";
                //browser.Navigate().GoToUrl(mandmURL);
                //ssnzMAndMServiceLoaded = (browser.Url == mandmURL);

            }

            //Assert
            Assert.IsTrue(ssnzWebLoaded);
            Assert.IsTrue(ssnzSteamServiceLoaded);
            Assert.IsTrue(ssnzFFServiceLoaded);
            Assert.IsTrue(ssnzIntFootballServiceLoaded);
            Assert.IsTrue(ssnzITunesServiceLoaded);
            Assert.IsTrue(ssnzGuitarTabServiceLoaded);
            Assert.IsTrue(ssnzMAndMServiceLoaded == false);

        }
    }
}

