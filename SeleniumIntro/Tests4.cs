using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Linq;

namespace SeleniumIntro
{
    public class Tests4
    {
        /// <summary>
        /// Examples: Timeouts
        /// </summary>
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

        #region Test10
        [Test]
        public void JuiceShop_LocatingElementsTest()
        {
            driver.Navigate().GoToUrl("https://juice-shop.herokuapp.com/");
            driver.FindElement(By.XPath("//app-welcome-banner//span[text()='Dismiss']")).Click();
            IWebElement searchIcon = driver.FindElement(By.CssSelector(".mat-search_icon-search"));
            IWebElement searchInput = driver.FindElement(By.TagName("input"));
            string searchPhrase = "carrot";
            searchIcon.Click();
            searchInput.SendKeys(searchPhrase);
            searchInput.SendKeys(Keys.Enter);

            // Explicit wait: waits for element to be displayed
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(d => d.FindElement(By.XPath("//mat-grid-list")).Displayed);
            //wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//mat-grid-list"))); // WaitHelpers package

            var searchResultGridItem = driver.FindElement(By.XPath("//mat-grid-list/div"));
            var searchResultList = searchResultGridItem.FindElements(By.ClassName("mat-grid-tile"));
            var searchValueHeader = driver.FindElement(By.Id("searchValue")).Text;
            var searchResultCount = searchResultList.Count;
            var searchResultItem = searchResultList.First().Text;
            Assert.Multiple(() =>
            {
                StringAssert.Contains(searchPhrase, searchResultItem.ToLower());
                StringAssert.AreEqualIgnoringCase(searchPhrase, searchValueHeader);
                Assert.AreEqual(1, searchResultCount, "Search count doesn't match expected value.");
            });

        }
        #endregion

        #region Test11
        [Test]
        public void AutomationPractice_LocatingElementsTest()
        {
            driver.Navigate().GoToUrl("http://automationpractice.com");
            IWebElement searchInput = driver.FindElement(By.Id("search_query_top"));
            string searchPhrase = "t-shirt";
            searchInput.SendKeys(searchPhrase);
            searchInput.Submit();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("heading-counter")));

            var searchResultList = driver.FindElements(By.CssSelector(".product_list > li"));
            var searchValueHeader = driver.FindElement(By.CssSelector("#center_column > h1.page-heading > span.lighter")).Text;
            var searchResultCount = searchResultList.Count;
            var searchResultItem = searchResultList.First().Text;
            Assert.Multiple(() =>
            {
                StringAssert.Contains(searchPhrase, searchResultItem.ToLower());
                StringAssert.AreEqualIgnoringCase($"\"{searchPhrase}\"", searchValueHeader);
                Assert.AreEqual(1, searchResultCount, "Search count doesn't match expected value.");
            });

        }
        #endregion

        /// <summary>
        /// When test has state other than 'Success' make a screenshot and save file ingiven directory.
        /// Check if directory is created beforehand, otherwise it might produce an error.
        /// </summary>
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
    }
}
