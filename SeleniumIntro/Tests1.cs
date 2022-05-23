using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;

namespace SeleniumIntro
{
    [TestFixture]
    public class Tests1
    {
        /// <summary>
        /// Examples: IWebDriver class
        /// </summary>
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // install package Selenium.WebDriver.ChromeDriver
            driver = new ChromeDriver();
        }

        [TearDown]
        public void QuitChrome()
        {
            driver.Quit();
        }

        #region Test1
        /// <summary>
        /// Test naming: AreaName_Goal_ExpectedResult
        /// Arrange Act Assert pattern
        /// Example: Use Google to look up a phrase and open Wikipedia page from search results.
        /// Simple web elements' selectors and Selenium methods.
        /// </summary>
        [Test]
        public void Web_FindWikipediaPage_Found()
        {
            //Arrange
            Uri googleUrl = new Uri("https://google.pl");
            var cookieAgreeButtonId = "L2AGLb";
            string searchEntry = "asercja";
            //Act
            driver.Navigate().GoToUrl(googleUrl);
            driver.FindElement(By.Id(cookieAgreeButtonId)).Click();
            IWebElement searchField = driver.FindElement(By.CssSelector("[title='Szukaj']"));
            searchField.SendKeys(searchEntry);
            searchField.Submit();

            //Assert
            string title = "Asercja (informatyka) – Wikipedia, wolna encyklopedia";
            driver.FindElement(By.XPath(".//*[text()='" + title + "']")).Click();
            string entryURL = "https://pl.wikipedia.org/wiki/Asercja_(informatyka)";
            Assert.AreEqual(entryURL, driver.Url, "URL is not correct.");
        }
        #endregion

        #region Test2
        [Test]
        public void Web_RetrieveWikipediaTitle_Success()
        {
            //Arrange                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       
            var wikipediaUrl = "https://pl.wikipedia.org/";
            var expectedTitle = "Wikipedia, wolna encyklopedia";

            //Act
            driver.Navigate().GoToUrl(wikipediaUrl);

            //Assert
            Assert.AreEqual(expectedTitle, driver.Title, "Title is not correct");
        }
        #endregion
        
        #region Test3
        [Test]
        public void Web_RetrieveImageFromPageSource_Success()
        {
            driver.Navigate().GoToUrl("https://pl.wikipedia.org/wiki/Wikipedia");
            string metaContent = "//upload.wikimedia.org/wikipedia/commons/thumb/f/f6/Wikipedia-logo-v2-wordmark.svg/240px-Wikipedia-logo-v2-wordmark.svg.png";

            Assert.IsTrue(driver.PageSource.Contains(metaContent), "Meta content was not found in the source page.");            
        }
        #endregion

        #region Test4
        [Test]
        public void Options_ChangeWindowStartingPoint()
        {
            driver.Manage().Window.Position = new Point(50, 50);
            Point startingPoint = driver.Manage().Window.Position;
            Assert.AreEqual(new Point(50, 50), startingPoint, "Starting point is not (50,50)");
        }
        #endregion

        #region Test5
        [Test]
        public void Options_ChangeWindowStartingSize()
        {
            driver.Manage().Window.Size = new Size(945, 1020);
            Size startingSize = driver.Manage().Window.Size;
            Assert.AreEqual(new Size(945, 1020), startingSize, "Starting size is not (945, 1020)");
        }
        #endregion
    }
}