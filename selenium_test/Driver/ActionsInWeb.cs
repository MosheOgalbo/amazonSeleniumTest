
using OpenQA.Selenium;
using NUnit.Framework;

namespace DotnetSeleniumTest.Browser
{
    public class ActionsInWeb : DriverTest
    {
        private readonly IWebDriver? _driver;

        public ActionsInWeb()
        {
            this._driver = driver;
        }

        public void Screenshot()
        {
            try
            {
                //Thread.Sleep(1000);
                string? screenshotDirectory = "../../../Pngs";

                // Checking if the folder exists, if not - creating the folder
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                // Create a unique filename with date and time
                string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string screenshotPath = Path.Combine(screenshotDirectory, fileName);

                Screenshot screenshot = ((ITakesScreenshot)_driver!).GetScreenshot();

                screenshot.SaveAsFile(screenshotPath);
                TestContext.WriteLine($"Screenshot saved at: {screenshotPath}");

                // Adding the screenshot as an attachment to the test results
                TestContext.AddTestAttachment(screenshotPath);
                //Thread.Sleep(50);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public string GetScreenshot()
        {
            var file = ((ITakesScreenshot)_driver!).GetScreenshot();
            var img = file.AsBase64EncodedString;
            return img;
        }

    }
}
