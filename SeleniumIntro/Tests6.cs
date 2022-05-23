using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;

namespace SeleniumIntro
{
    public class Tests6
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
        /// <summary>
        /// Task:
        /// Launch the Chrome browser.
        /// Open URL - http://www.google.com
        /// Enter keyword "selenium c#" in search bar
        /// Wait for ajax suggestion box to appear
        /// (optional) Get/store all the options of suggestion box in a list
        /// Find "selenium c# wait for element" in options 
        /// Click on it using Actions "MoveByOffset"
        /// You'll need "OpenQA.Selenium.Interactions;"
        /// Click on link to https://www.lambdatest.com/ site from search results
        /// Assert URL.
        /// Check examples below.
        /// </summary>
        [Test] 
        public void Web_MoveToOffset()
        {
            Actions actions = new Actions(driver);
            int offsetX = 100;
            int offsetY = 50;
            driver.Navigate().GoToUrl("https://www.way2automation.com/way2auto_jquery/draggable.php#load_box");

            //Example how to switch to frame
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector("#example-1-tab-1 iframe")));

            IWebElement dragMeElement = driver.FindElement(By.Id("draggable"));
            Point originalLocation = dragMeElement.Location;

            /// Actions simulate user using mouse or keyboard.
            /// Remember to finish actions with ".Build().Perform();" otherwise they won't work.
            actions.ClickAndHold(dragMeElement).MoveByOffset(offsetX, offsetY).Release().Build().Perform();
            Point expectedLocation = new Point(originalLocation.X + offsetX, originalLocation.Y + offsetY);
            Assert.AreEqual(expectedLocation, dragMeElement.Location, "Element is not in expected location");
        }

        [Test]
        public void Web_DragElement()
        {
            Actions actions = new Actions(driver);
            driver.Navigate().GoToUrl("https://www.way2automation.com/way2auto_jquery/droppable.php#load_box");
            var frame = driver.FindElement(By.CssSelector("#example-1-tab-1 iframe"));
            driver.SwitchTo().Frame(frame);
            IWebElement dragMeElement = driver.FindElement(By.Id("draggable"));

            //Example how to scroll to element
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", dragMeElement);

            IWebElement dragToElement = driver.FindElement(By.Id("droppable"));
            actions.DragAndDrop(dragMeElement, dragToElement).Build().Perform();
            Assert.AreEqual("Dropped!", dragToElement.Text, "Draggable element wasn't dropped in the area.");
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
    }
}
