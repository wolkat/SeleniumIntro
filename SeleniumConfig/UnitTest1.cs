using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace SeleniumConfig
{
    [TestFixture]
    public class UnitTest1
    {
        /// <summary>
        /// Usage of different browser drivers
        /// </summary>
        IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(@"C:\repos\SeleniumConfig\Resources\");
            //driver = new FirefoxDriver(@"C:\repos\SeleniumConfig\Resources\");
            //driver = new EdgeDriver(@"C:\repos\SeleniumConfig\Resources\");
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }


        [Test]
        public void Test2()
        {
            var square = -1 * -1;
            Assert.That(square.Equals(1));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close(); // Compare in TaskManager
            driver.Quit();
        }
    }
}