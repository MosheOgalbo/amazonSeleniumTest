using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DotnetSeleniumTest.Driver
{
    public static class Driver
    {
        //  private static readonly IWebDriver? _driver;
        // private  static readonly WebDriverWait? wait;
        // יצירת WebDriver עבור Chrome
        public static IWebDriver Initialize(string url)
        {
            IWebDriver driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            TransitionBrowser(driver, url);
            driver.Navigate().Refresh();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //   if(driver.Url==url){
            //     Cleanup(driver);
            //     return driver;
            //     }else{
            //         return driver;
                //}
            return driver;
        }

        public static void TransitionBrowser(IWebDriver driver, string url){
            try{
                driver.Navigate().GoToUrl(url);

            }catch(Exception e){
                Console.WriteLine(e);
                 throw new NotImplementedException();
            }
        }
        public static  void Cleanup(IWebDriver driver)
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}
