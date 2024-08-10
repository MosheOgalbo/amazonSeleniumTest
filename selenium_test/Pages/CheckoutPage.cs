using DotnetSeleniumTest.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotnetSeleniumTest.Pages
{
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        private readonly ActionsInWeb _actions;

        // אתחול של דף התשלום
        public CheckoutPage(IWebDriver driver)
        {
            this._driver = driver;
             _actions = new ActionsInWeb();

        }

        // פונקציה לצילום מסך
        public void MakingPaymentForProduct()
        {
           //Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            _actions.Screenshot();
        }
    }
}
