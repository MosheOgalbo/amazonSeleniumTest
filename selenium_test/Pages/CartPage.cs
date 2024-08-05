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
        private IReadOnlyList <IWebElement> GetElements(By by) => _driver.FindElements(by);


        // פונקציה לאימות כמות פריטי העגלה
        public  bool VerifyCart()
        {
            _wait.UnitToElementIsClick(byProductInCart);
            string cartIconLogoText= CartIconLogo.Text;
            int numberItemsList=GetElements(byProductInCart).Count();
           // בדוק אם הטקסט אינו ריק
           if (!string.IsNullOrWhiteSpace(cartIconLogoText)|| numberItemsList!=0)
           {
            // נסה להמיר את הטקסט למספר
            if (int.TryParse(cartIconLogoText, out int cartIconLogoValue))
            {
                if (numberItemsList==cartIconLogoValue){
                    return true;
                }else{
                    return false;
                }
            }
            else{
            // אם ההמרה נכשלה, הודע על כך
            Console.WriteLine($"הטקסט '{cartIconLogoText}' אינו מספר.");
            return false;
            }

        }
        return true;
    }

        // פונקציה להמשך לתשלום
        public void ProceedToCheckout()
        {
            CheckoutButton.Click(); // לוחץ על כפתור ההמשך לתשלום
        }

        public void ProceedToPayForProduct()
        {
            _wait.UnitToElementIsClick(byProductInCart).Click();
            if (VerifyCart()) {
                ProceedToCheckout();
            }
            else
            {
                Console.WriteLine("Payment cannot be continued because there is no product");
            }
            //_wait.UntilElementIsRemoved(byProductInCart);
        }
    }
}
