using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotnetSeleniumTest.Driver
{
 public class WaitDriver{
         private  readonly IWebDriver? driver;
         public WaitDriver(IWebDriver driver){
            this.driver = driver;
         }
//  עד שאלמנט יוצג המתנה לחלון חדש
        public  IWebElement UnitToElementIsClick(By locator){
              WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
             return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
        }
         public bool UntilElementIsRemoved(By locator){
             WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
           return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
         }


    }
}
