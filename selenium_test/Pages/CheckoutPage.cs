using DotnetSeleniumTest.Browser;


namespace DotnetSeleniumTest.Pages
{
    public class CheckoutPage : Browser.DriverTest
    {
        //private readonly IWebDriver? _driver;
        //private readonly WebDriverWait? _wait;
        private readonly ActionsInWeb? _actions;

        public CheckoutPage()
        {
            _actions = new ActionsInWeb();
        }

        // Function to take a screenshot
        public void MakingPaymentForProduct()
        {
            _actions?.Screenshot();
        }
    }
}
