using DotnetSeleniumTest.Browser;
using OpenQA.Selenium;
using selenium_test;

namespace DotnetSeleniumTest.Pages
{
    public class CartPage : DriverTest
    {
        //private readonly IWebDriver? driver;
        private readonly WaitDriver _wait;

        // Initialize the cart page
        public CartPage()
        {
            //this._driver = Driver.Driver._driver;
            //_wait = new WaitDriver(_driver);
            _wait = new WaitDriver();
        }

        // components on the page
        private IWebElement CartIconLogo => Browser.DriverTest.driver!.FindElement(By.Id("nav-cart")); // אייקון העגלה
        private IWebElement CheckoutButton => Browser.DriverTest.driver!.FindElement(By.Id("desktop-ptc-button-celWidget")); // כפתור ההמשך לתשלום
        private By byProductInCart => By.XPath("//*[@data-bundleitem='absent']");
        private IReadOnlyList<IWebElement> GetElements(By by) => driver!.FindElements(by);

        // Function to verify the amount of cart items
        public bool VerifyCart()
        {
            _wait.UnitToElementIsClick(byProductInCart);
            string cartIconLogoText = CartIconLogo.Text;
            int numberItemsList = GetElements(byProductInCart).Count();

            // Check if the text is not empty
            if (!string.IsNullOrWhiteSpace(cartIconLogoText) || numberItemsList != 0)
            {
                // tried to convert the text to a number
                if (int.TryParse(cartIconLogoText, out int cartIconLogoValue))
                {
                    if (numberItemsList == cartIconLogoValue)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    // If the conversion failed, report it
                    Console.WriteLine($"הטקסט '{cartIconLogoText}' אינו מספר.");
                    return false;
                }

            }
            return true;
        }

        // continue function to pay by pressing a button to pass payment
        public void ProceedToCheckout()
        {
            CheckoutButton.ClickInElement();
        }

        public void ProceedToPayForProduct()
        {
            _wait.UnitToElementIsClick(byProductInCart)?.Click();
            if (VerifyCart())
            {
                Console.WriteLine("Payment can be continued");
            }
            else
            {
                Console.WriteLine("Payment cannot be continued because there is no product");
            }
            ProceedToCheckout();
            Thread.Sleep(10);
        }
    }
}
