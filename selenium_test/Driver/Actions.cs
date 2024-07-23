
using OpenQA.Selenium;
using NUnit.Framework;

namespace DotnetSeleniumTest.Driver
{
    public  class Actions{
        private readonly IWebDriver? driver;
          public  Actions( IWebDriver driver){
            this.driver = driver;
        }
        public  void Screenshot(){
            try{
                string screenshotDirectory = "../../../Pngs";
                    // בדיקה אם התיקייה קיימת, אם לא - יצירת התיקייה
                if (!Directory.Exists(screenshotDirectory)){
                        Directory.CreateDirectory(screenshotDirectory);
                    }

                    // יצירת שם קובץ ייחודי עם תאריך ושעה
                    string fileName = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                    string screenshotPath = Path.Combine(screenshotDirectory, fileName);

                    Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

                    screenshot.SaveAsFile(screenshotPath);
                    TestContext.WriteLine($"Screenshot saved at: {screenshotPath}");

                    // הוספת הצילום מסך כקובץ מצורף לתוצאות הבדיקה
                    TestContext.AddTestAttachment(screenshotPath);

            }catch(Exception e){
                Console.WriteLine(e);
            }
        }

        internal object ClickAndHold(IWebElement priceFilter)
        {
            throw new NotImplementedException();
        }
    }
}
