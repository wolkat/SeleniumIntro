using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace SeleniumIntro
{
    public class Tests3
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

        #region Test9
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

        [TearDown]
        public void QuitChrome()
        {
            driver.Quit();
        }
    }
}
