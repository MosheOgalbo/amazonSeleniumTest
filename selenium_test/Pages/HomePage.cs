using DotnetSeleniumTest.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public  class HomePage
    {
        private readonly IWebDriver driver;
          private readonly WaitDriver _wait;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            _wait = new WaitDriver(this.driver);
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
