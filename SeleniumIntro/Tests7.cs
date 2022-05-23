using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace SeleniumIntro
{
    public class Tests7
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

        /// Task: Automate registration and buying processes in http://automationpractice.com/
        /// Prepare at least 3 scripts. For inspiration check Tests4.cs -> Test11
    }
}
