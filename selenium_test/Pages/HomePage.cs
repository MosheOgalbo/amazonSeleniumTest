using DotnetSeleniumTest.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public  class HomePage
    {
        private readonly IWebDriver driver;
          private readonly WaitDriver webScreenWait;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;

            // _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
       private IWebElement SearchBox => driver.FindElement(By.Id("twotabsearchtextbox"));
       private IWebElement SearchButton => driver.FindElement(By.Id("nav-search-submit-button"));
        private By bySearchBox => By.Id("twotabsearchtextbox");

        // חיפוש מוצר לפי מונח חיפוש
        public void SearchForItem(string item)
        {
            WaitDriver waitDriver = new WaitDriver(driver);
            waitDriver.WebScreenWait(bySearchBox);
            SearchBox.EnterText(item);
            SearchButton.ClickInElement();
        }

    }
}
