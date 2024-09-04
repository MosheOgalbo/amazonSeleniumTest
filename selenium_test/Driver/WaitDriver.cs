using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace DotnetSeleniumTest.Browser
{
    public class WaitDriver : DriverTest
    {
        public WaitDriver()
        {

        }

        public IWebElement? UnitToElementIsClick(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(locator));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return null;
        }

        public bool UntilElementIsRemoved(By locator)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

    }
}
