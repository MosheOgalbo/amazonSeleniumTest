using DotnetSeleniumTest.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public class HomePage : Browser.DriverTest
    {
        //private readonly IWebDriver? _driver;
        private readonly WaitDriver _wait;
        public HomePage()
        {
            //this.driver = _driver;
            //_wait = new WaitDriver(this.driver);
            _wait = new WaitDriver();
        }
        private By bySearchBox => By.Id("twotabsearchtextbox");
        private IWebElement SearchBox => Browser.DriverTest.driver!.FindElement(bySearchBox);
        private IWebElement SearchButton => Browser.DriverTest.driver!.FindElement(By.Id("nav-search-submit-button"));

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
