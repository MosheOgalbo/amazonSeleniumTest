using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public class SearchResultsPage
    {
        private IWebDriver driver;
        public SearchResultsPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
        private  IWebElement _priceFilter =driver(By.XPath("//span[contains(text(), 'Under $500')]"));
        private IWebElement _memoryFilter = driver(By.XPathBy.XPath("//span[contains(text(), '16 GB')]"));
        private IWebElement _ratingFilter = driver(By.XPathBy.XPath("//span[contains(text(), '4 Stars & Up')]"));

    }
}
