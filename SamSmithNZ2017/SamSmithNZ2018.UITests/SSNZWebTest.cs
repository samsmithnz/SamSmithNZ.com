using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace SamSmithNZ2018.UITests
{
    [TestClass]
    public class SSNZWebTest
    {
        private ChromeDriver _Driver;
        private TestContext testContextInstance;

        private string _SSNZWebUrl = null;
        private string _SSNZSteamServiceUrl = null;
        private string _SSNZFFServiceUrl = null;
        private string _SSNZIntFootballServiceUrl = null;
        private string _SSNZITunesServiceUrl = null;
        private string _SSNZGuitarTabServiceUrl = null;
        private string _SSNZMAndMServiceUrl = null;
        private string _SSNZLegoServiceUrl = null;

        [TestInitialize]
        public void SetupTests()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            //chromeOptions.AddArgument("no-sandbox");
            _Driver = new ChromeDriver(chromeOptions);

            if (TestContext.Properties == null || TestContext.Properties.Count == 0)
            {
                _SSNZWebUrl = "https://www.samsmithnz.com/";
                _SSNZSteamServiceUrl = "https://ssnzsteam2019webservice.azurewebsites.net/";
                _SSNZFFServiceUrl = "https://ssnzfoofighterswebservice.azurewebsites.net/";
                _SSNZIntFootballServiceUrl = "https://ssnzintfootballwebservice.azurewebsites.net/";
                _SSNZITunesServiceUrl = "https://ssnzituneswebservice.azurewebsites.net/";
                _SSNZGuitarTabServiceUrl = "https://ssnzguitartabservice.azurewebsites.net/";
                _SSNZMAndMServiceUrl = "https://ssnzmandmcounterservice.azurewebsites.net/";
                _SSNZLegoServiceUrl = "https://ssnzlegowebservice.azurewebsites.net/";
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
                _SSNZLegoServiceUrl = TestContext.Properties["SSNZLegoServiceUrl"].ToString();
            }
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZHomePageTest()
        {
            //Arrange
            bool ssnzWebLoaded = false;

            //Act
            _Driver.Navigate().GoToUrl(this._SSNZWebUrl);
            ssnzWebLoaded = (_Driver.Url == this._SSNZWebUrl);
            //Assert.AreEqual("AppStack", Driver.FindElement(By.ClassName("navbar-brand")).Text);

            //Assert
            Assert.IsTrue(ssnzWebLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZSteamServiceTest()
        {
            //Arrange
            bool ssnzSteamServiceLoaded = false;

            //Act
            string steamURL = this._SSNZSteamServiceUrl + "api/Player/GetPlayer?steamId=76561197971691578";
            _Driver.Navigate().GoToUrl(steamURL);
            ssnzSteamServiceLoaded = (_Driver.Url == steamURL);

            //Assert
            Assert.IsTrue(ssnzSteamServiceLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZFFServiceTest()
        {
            //Arrange
            bool ssnzFFServiceLoaded = false;

            //Act
            string ffURL = this._SSNZFFServiceUrl + "";
            _Driver.Navigate().GoToUrl(ffURL);
            ssnzFFServiceLoaded = (_Driver.Url == ffURL);

            //Assert
            Assert.IsTrue(ssnzFFServiceLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZIntFootballServiceTest()
        {
            //Arrange
            bool ssnzIntFootballServiceLoaded = false;

            //Act
            string intFootballURL = this._SSNZIntFootballServiceUrl + "";
            _Driver.Navigate().GoToUrl(intFootballURL);
            ssnzIntFootballServiceLoaded = (_Driver.Url == intFootballURL);

            //Assert
            Assert.IsTrue(ssnzIntFootballServiceLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZITunesServiceTest()
        {
            //Arrange
            bool ssnzITunesServiceLoaded = false;

            //Act
            string itunesURL = this._SSNZITunesServiceUrl + "";
            _Driver.Navigate().GoToUrl(itunesURL);
            ssnzITunesServiceLoaded = (_Driver.Url == itunesURL);

            //Assert
            Assert.IsTrue(ssnzITunesServiceLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZGuitarTabServiceTest()
        {
            //Arrange
            bool ssnzGuitarTabServiceLoaded = false;

            //Act
            string guitartabURL = this._SSNZGuitarTabServiceUrl + "";
            _Driver.Navigate().GoToUrl(guitartabURL);
            ssnzGuitarTabServiceLoaded = (_Driver.Url == guitartabURL);

            //Assert
            Assert.IsTrue(ssnzGuitarTabServiceLoaded);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZMandMServiceTest()
        {
            //Arrange
            bool ssnzMAndMServiceLoaded = false;

            //Act
            string mandmURL = this._SSNZMAndMServiceUrl + "api/MandMCounter/GetDataForUnit?unit=cup&quantity=1";
            _Driver.Navigate().GoToUrl(mandmURL);
            ssnzMAndMServiceLoaded = (_Driver.Url == mandmURL);

            //Assert
            Assert.IsTrue(ssnzMAndMServiceLoaded == true);
        }

        [TestMethod]
        [TestCategory("SkipWhenLiveUnitTesting")]
        [TestCategory("SmokeTest")]
        public void GotoSSNZLegoServiceTest()
        {
            //Arrange
            bool ssnzLegoServiceLoaded = false;

            //Act
            string legoURL = this._SSNZLegoServiceUrl + "api/LegoOwnedSets/GetOwnedSets"; ;
            _Driver.Navigate().GoToUrl(legoURL);
            ssnzLegoServiceLoaded = (_Driver.Url == legoURL);

            //Assert
            Assert.IsTrue(ssnzLegoServiceLoaded == true);
        }

        [TestCleanup()]
        public void CleanupTests()
        {
            _Driver.Quit();
        }

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        //[TestMethod]
        //[TestCategory("SkipWhenLiveUnitTesting")]
        //[TestCategory("SmokeTest")]
        //public void GotoSSNZHomePage()
        //{
        //    //Arrange
        //    bool ssnzWebLoaded = false;
        //    bool ssnzSteamServiceLoaded = false;
        //    bool ssnzFFServiceLoaded = false;
        //    bool ssnzIntFootballServiceLoaded = false;
        //    bool ssnzITunesServiceLoaded = false;
        //    bool ssnzGuitarTabServiceLoaded = false;
        //    bool ssnzMAndMServiceLoaded = false;

        //    //Act
        //    _Driver.Navigate().GoToUrl(this._SSNZWebUrl);
        //    ssnzWebLoaded = (_Driver.Url == this._SSNZWebUrl);
        //    //Assert.AreEqual("AppStack", Driver.FindElement(By.ClassName("navbar-brand")).Text);

        //    string steamURL = this._SSNZSteamServiceUrl + "api/Player/GetPlayer?steamId=76561197971691578";
        //    _Driver.Navigate().GoToUrl(steamURL);
        //    ssnzSteamServiceLoaded = (_Driver.Url == steamURL);

        //    string ffURL = this._SSNZFFServiceUrl + "";
        //    _Driver.Navigate().GoToUrl(ffURL);
        //    ssnzFFServiceLoaded = (_Driver.Url == ffURL);

        //    string intFootballURL = this._SSNZIntFootballServiceUrl + "";
        //    _Driver.Navigate().GoToUrl(intFootballURL);
        //    ssnzIntFootballServiceLoaded = (_Driver.Url == intFootballURL);

        //    string itunesURL = this._SSNZITunesServiceUrl + "";
        //    _Driver.Navigate().GoToUrl(itunesURL);
        //    ssnzITunesServiceLoaded = (_Driver.Url == itunesURL);

        //    string guitartabURL = this._SSNZGuitarTabServiceUrl + "";
        //    _Driver.Navigate().GoToUrl(guitartabURL);
        //    ssnzGuitarTabServiceLoaded = (_Driver.Url == guitartabURL);

        //    //string mandmURL = this._SSNZMAndMServiceUrl + "";
        //    //_Driver.Navigate().GoToUrl(mandmURL);
        //    //ssnzMAndMServiceLoaded = (_Driver.Url == mandmURL);

        //    //Assert
        //    Assert.IsTrue(ssnzWebLoaded);
        //    Assert.IsTrue(ssnzSteamServiceLoaded);
        //    Assert.IsTrue(ssnzFFServiceLoaded);
        //    Assert.IsTrue(ssnzIntFootballServiceLoaded);
        //    Assert.IsTrue(ssnzITunesServiceLoaded);
        //    Assert.IsTrue(ssnzGuitarTabServiceLoaded);
        //    Assert.IsTrue(ssnzMAndMServiceLoaded == false);
        //}
    }
}

