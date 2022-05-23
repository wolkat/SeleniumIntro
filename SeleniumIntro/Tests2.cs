using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumIntro
{
    public class Tests2
    {
        /// <summary>
        /// Examples: INavigation class
        /// </summary>
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        #region Test6
        [Test]
        public void Navigation_ClickBack_PreviosPageIsDisplayed()
        {
            string wikipediaUrl = "https://pl.wikipedia.org/";
            string amazonUrl = "https://amazon.com";
            driver.Navigate().GoToUrl(wikipediaUrl);
            driver.Navigate().GoToUrl(amazonUrl);
            driver.Navigate().Back();
            string expectedUrl = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";
            Assert.AreEqual(expectedUrl, driver.Url, "Url is not correct");
        }
        #endregion

        /// <summary>
        /// Task: Open Wikipidia homepage, then navigate to Allegro homepage. Use methods Back and Forward to go back to Wikipedia. Assert url.
        /// </summary>
        #region Test7
        [Test]
        public void Navigation_ClickForward_PageIsDisplayed()
        {
            #region Solution: DO NOT LOOK UP BEFORE COMPLETING THE TASK
            string wikipediaUrl = "https://pl.wikipedia.org/";
            string allegroUrl = "https://allegro.pl";

            driver.Navigate().GoToUrl(allegroUrl);
            driver.Navigate().GoToUrl(wikipediaUrl);
            driver.Navigate().Back();
            driver.Navigate().Forward();
            string expectedUrl = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";
            Assert.AreEqual(expectedUrl, driver.Url, "Url is not correct");
            #endregion
        }
        #endregion

        #region Test8
        [Test]
        public void Navigation_ClickRefresh_Success()
        {
            string allegroUrl = "https://allegro.pl";
            driver.Navigate().GoToUrl(allegroUrl);
            driver.Navigate().Refresh(); // Add breakpoint here. Run in debug.

            #region Window size manipulation
            driver.Manage().Window.Maximize();
            driver.Manage().Window.Minimize();
            driver.Manage().Window.FullScreen();
            #endregion
        }
        #endregion

        [TearDown]
        public void QuitChrome()
        {
            driver.Quit();
        }
    }
}
