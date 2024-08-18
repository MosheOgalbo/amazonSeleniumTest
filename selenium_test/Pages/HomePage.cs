using DotnetSeleniumTest.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public  class HomePage:Driver.Driver
    {
        //private readonly IWebDriver? _driver;
          private readonly WaitDriver _wait;
        public HomePage()
        {
            //this.driver = _driver;
            //_wait = new WaitDriver(this.driver);
            _wait = new WaitDriver();
        }
       private IWebElement SearchBox => driver.FindElement(By.Id("twotabsearchtextbox"));
       private IWebElement SearchButton => driver.FindElement(By.Id("nav-search-submit-button"));
        private By bySearchBox => By.Id("twotabsearchtextbox");

        // חיפוש מוצר לפי מונח חיפוש
        public void SearchForItem(string item)
        {
             _wait.UnitToElementIsClick(bySearchBox);
            // WaitDriver waitDriver = new WaitDriver(driver);
             _wait.UnitToElementIsClick(bySearchBox);
            SearchBox.EnterText(item);
            SearchButton.ClickInElement();
            _wait.UnitToElementIsClick(bySearchBox);

        }

    }
}
