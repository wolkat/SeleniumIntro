using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumIntro
{
    public class Tests5
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            #region Options
            //Wait x seconds to find the element and then fail, x = 5 here
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            //Wait 30 seconds to load web page
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            #endregion
        }

        [TearDown]
        public void QuitChrome()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                Screenshot image = ((ITakesScreenshot)driver).GetScreenshot();
                string dateTime = DateTime.Now.ToString("yyyy-MM-dd HHmmss");
                string testName = TestContext.CurrentContext.Test.Name;
                string filePath = @"D:\SeleniumIntroScreenshots\" + testName + " " + dateTime + ".png";
                image.SaveAsFile(filePath);
                Console.WriteLine($"{testName} failed, check screenshot at: {filePath}");
            }
            driver.Quit();
        }

        /// <summary>
        /// Task: Switch window handler using example on http://www.seleniumframework.com/Practiceform/
        /// use driver.CurrentWindowHandle and driver.SwitchTo().Window(windowHandle);
        /// Check examples below.
        /// </summary>
        [Test]
        public void Web_OpenSecondWindow()
        {
            string wikipediaUrl = "https://pl.wikipedia.org/";
            string allegroUrl = "https://allegro.pl";
            driver.Navigate().GoToUrl(wikipediaUrl);
            driver.SwitchTo().NewWindow(WindowType.Window).Navigate().GoToUrl(allegroUrl);
            Assert.AreEqual(driver.WindowHandles.Count, 2);
        }

        [Test]
        public void Web_SwithBackToFirstWindow()
        {
            string wikipediaUrl = "https://pl.wikipedia.org/";
            string allegroUrl = "https://allegro.pl";
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            driver.Navigate().GoToUrl(wikipediaUrl);
            string originalWindow = driver.CurrentWindowHandle;
            driver.SwitchTo().NewWindow(WindowType.Window).Navigate().GoToUrl(allegroUrl);
            wait.Until(d => d.WindowHandles.Count == 2);
            driver.SwitchTo().Window(originalWindow);
            string expectedUrl = "https://pl.wikipedia.org/wiki/Wikipedia:Strona_g%C5%82%C3%B3wna";
            Assert.AreEqual(expectedUrl, driver.Url, "Url is not correct");
        }
    }
}
