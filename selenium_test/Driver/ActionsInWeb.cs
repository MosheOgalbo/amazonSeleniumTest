
using OpenQA.Selenium;
using NUnit.Framework;

namespace DotnetSeleniumTest.Browser
{
    public class ActionsInWeb : DriverTest
    {
        private readonly IWebDriver? _driver;
        public ActionsInWeb()
        {
            this._driver = Browser.DriverTest.driver;
        }
        public void Screenshot()
        {
            try
            {

                Thread.Sleep(100);
                string screenshotDirectory = "../../../Pngs";
                // בדיקה אם התיקייה קיימת, אם לא - יצירת התיקייה
                if (!Directory.Exists(screenshotDirectory))
                {
                    Directory.CreateDirectory(screenshotDirectory);
                }

                // יצירת שם קובץ ייחודי עם תאריך ושעה
                string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string screenshotPath = Path.Combine(screenshotDirectory, fileName);

                Screenshot screenshot = ((ITakesScreenshot)_driver!).GetScreenshot();

                screenshot.SaveAsFile(screenshotPath);
                TestContext.WriteLine($"Screenshot saved at: {screenshotPath}");

                // הוספת הצילום מסך כקובץ מצורף לתוצאות הבדיקה
                TestContext.AddTestAttachment(screenshotPath);
                Thread.Sleep(1000);

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
