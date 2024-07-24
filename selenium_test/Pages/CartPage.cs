using DotnetSeleniumTest.Driver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DotnetSeleniumTest.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
         private readonly WaitDriver _wait;

        // אתחול של דף העגלה
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
             _wait = new WaitDriver(_driver);
        }

        // רכיבים בדף
        private IWebElement CartIconLogo => _driver.FindElement(By.Id("nav-cart")); // אייקון העגלה
        private IWebElement CheckoutButton => _driver.FindElement(By.Id("desktop-ptc-button-celWidget")); // כפתור ההמשך לתשלום
        private By byProductInCart => By.XPath("//*[@data-bundleitem='absent']");

        // פונקציה לאימות כמות פריטי העגלה
        public void VerifyCart()
        {
            _wait.WebScreenWait(byProductInCart);
            string CartIconLogoText=CartIconLogo.Text.ToString();

        }

        // פונקציה להמשך לתשלום
        public void ProceedToCheckout()
        {
            CheckoutButton.Click(); // לוחץ על כפתור ההמשך לתשלום
        }
    }
}
