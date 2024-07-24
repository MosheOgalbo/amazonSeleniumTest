using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace DotnetSeleniumTest.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // אתחול של דף העגלה
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            // _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // רכיבים בדף
        private IWebElement CartIcon => _driver.FindElement(By.Id("")); // אייקון העגלה
        private IWebElement CheckoutButton => _driver.FindElement(By.XPath("")); // כפתור ההמשך לתשלום

        // פונקציה לאימות פריטי העגלה
        public void VerifyCart()
        {
        }

        // פונקציה להמשך לתשלום
        public void ProceedToCheckout()
        {
            CheckoutButton.Click(); // לוחץ על כפתור ההמשך לתשלום
        }
    }
}
