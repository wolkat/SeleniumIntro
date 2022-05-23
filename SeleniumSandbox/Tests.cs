using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumSandbox
{
    public class Tests
    {
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [Test]
        public void Test1()
        {
            var sum = 1 + 2;
            Assert.AreEqual(3, sum, "Test failed because I don't know math");
        }

        [Test]
        public void Web_FindWikipediaPage_Found()
        {
            string googleUrlString = "https://google.pl";
            var cookieAgreeButtonId = "L2AGLb";
            driver.Navigate().GoToUrl(googleUrlString);
            var cookieAgreeButton = driver.FindElement(By.Id(cookieAgreeButtonId));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", cookieAgreeButton);
            cookieAgreeButton.Click();
            IWebElement searchField = driver.FindElement(By.CssSelector("[title='Szukaj']"));
            string searchEntry = "asercja";
            searchField.SendKeys(searchEntry);
            searchField.Submit();
            string title = "Asercja (informatyka) – Wikipedia, wolna encyklopedia";
            driver.FindElement(By.XPath(".//*[text()='" + title + "']")).Click();
            string entryURL = "https://pl.wikipedia.org/wiki/Asercja_(informatyka)";
            Assert.AreEqual(entryURL, driver.Url, "URL is not correct.");
        }

        [TearDown]
        public void TearDown()
        {
            //driver.Close();
            driver.Quit();
        }
    }
}