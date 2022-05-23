using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumConfig
{
    [TestFixture]
    public class UnitTest2
    {
        /// <summary>
        /// Exaple of NUnit attributes' usage
        /// </summary>
        IWebDriver driver;

        public const string DRIVER_PATH = @"C:\repos\SeleniumConfig\Resources\";

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(DRIVER_PATH);
        }

        [Test, Order(0)]
        [Category("Smoke")] 
        [Description("This is test description")]
        public void Test1()
        {
            Assert.Pass();
        }

        [TestCase(1)]
        [TestCase(-1)]
        [Category("Smoke"), Description("This is test description")]
        //[Ignore("Do not run this test", Until = "2022-05-23")]
        public void Test2(int number)
        {
            var square = number * number;
            Assert.That(square.Equals(1));
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
            driver.Quit();
        }
    }
}