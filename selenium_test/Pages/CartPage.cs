using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AmazonAutomation.Pages
{
    public class CartPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // אתחול של דף העגלה
        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // רכיבים בדף
        private IWebElement CartIcon => _driver.FindElement(By.Id("nav-cart")); // אייקון העגלה
        private IWebElement CheckoutButton => _driver.FindElement(By.Name("proceedToRetailCheckout")); // כפתור ההמשך לתשלום

        // פונקציה לאימות פריטי העגלה
        public void VerifyCart()
        {
            CartIcon.Click(); // לוחץ על אייקון העגלה
            _wait.Until(d => d.Url.Contains("cart")); // ממתין לטעינת דף העגלה
        }

        // פונקציה להמשך לתשלום
        public void ProceedToCheckout()
        {
            CheckoutButton.Click(); // לוחץ על כפתור ההמשך לתשלום
            _wait.Until(d => d.Url.Contains("checkout")); // ממתין לטעינת דף התשלום
        }
    }
}
