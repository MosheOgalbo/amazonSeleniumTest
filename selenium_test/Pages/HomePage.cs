using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public  class HomePage
    {
        private readonly IWebDriver driver;
        // private readonly WebDriverWait _wait;
        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            // _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
        IWebElement SearchBox => driver.FindElement(By.Id("twotabsearchtextbox"));
        IWebElement SearchButton => driver.FindElement(By.Id("nav-search-submit-button"));


        // חיפוש מוצר לפי מונח חיפוש
        public void SearchForItem(string item)
        {
            SearchBox.EnterText(item);
            SearchButton.ClickInElement();
        }

    }
}
