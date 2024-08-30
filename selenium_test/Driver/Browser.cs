using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotnetSeleniumTest.Browser
{
    public class DriverTest
    {
        public static IWebDriver? driver;
        // private  static readonly WebDriverWait? wait;
        // יצירת WebDriver עבור Chrome
        public static IWebDriver Initialize(string url)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            TransitionBrowser(url);
            driver.Navigate().Refresh();
            Thread.Sleep(2000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            // Driver.webDriver = webDriver;
            return driver;
        }

        public static void TransitionBrowser(string url)
        {
            try
            {
                driver?.Navigate().GoToUrl(url);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new NotImplementedException();
            }
        }
        public static void Cleanup()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
