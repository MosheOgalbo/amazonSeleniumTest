using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotnetSeleniumTest.Driver
{
    public  class Driver
    {
         public  static IWebDriver? webDriver;
        // private  static readonly WebDriverWait? wait;
        // יצירת WebDriver עבור Chrome
        public static IWebDriver Initialize(string url)
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Window.Maximize();
            Thread.Sleep(5000);
            TransitionBrowser(webDriver, url);
            webDriver.Navigate().Refresh();
            Thread.Sleep(2000);
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            // Driver.webDriver = webDriver;
            return webDriver;
        }

        public static void TransitionBrowser(IWebDriver driver, string url){
            try{
                driver.Navigate().GoToUrl(url);

            }catch(Exception e){
                Console.WriteLine(e);
                 throw new NotImplementedException();
            }
        }
        public static  void Cleanup()
        {
            if (webDriver != null)
            {
                webDriver.Quit();
            }
        }
    }
}
