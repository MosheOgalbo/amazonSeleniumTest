using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotnetSeleniumTest.Browser
{
    public class DriverTest
    {
        public static IWebDriver? driver;
        // private  static readonly WebDriverWait? wait;

        public static IWebDriver Initialize(string url = "http://localhost")
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            RefreshDriver();
            TransitionBrowser(url);
            RefreshDriver();
            Thread.Sleep(5000);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
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

        public static void RefreshDriver()
        {
            driver?.Navigate().Refresh();
            Thread.Sleep(100);
        }

        public static IWebElement? GetElementIfExists(By by)
        {
            try
            {
                IWebElement webElement = driver!.FindElement(by);
                return webElement;

            }
            catch (NoSuchElementException)
            {
                // במקרה שהאלמנט לא נמצא, החזר null
                return null;
            }
            catch (FormatException)
            {
                // במקרה של בעיית עיצוב, החזר null
                return null;
            }
        }
    }

}
