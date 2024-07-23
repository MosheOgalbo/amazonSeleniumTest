using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace selenium_test
{
    public static class SeleniumCustomMethods
    {
        public static void ClickInElement(this IWebElement locator){
           locator.Click();
        }
        public static void EnterText(this IWebElement  locator ,string text){
           locator.Clear();
           locator.SendKeys(text);
        }
        public static void SelectDropdownByText(this IWebElement locator, string text){
           SelectElement selectElement= new SelectElement(locator);
           selectElement.SelectByText(text);
        }
            public static void SelectDropdownByValue(this IWebElement locator, string value){
           SelectElement selectElement= new SelectElement(locator);
           selectElement.SelectByValue(value);
        }
        public static void MultiSelectElements(this IWebElement locator, string[] values){
           SelectElement multiSelect= new SelectElement(locator);
           foreach(var value in values){
               multiSelect.SelectByValue(value);
           }
        }
     public static List<string> GetAllSelectedOptions(this IWebElement locator){
        List<string> options = new List<string>();
        SelectElement selectElement= new SelectElement(locator);
        IList<IWebElement> SelectOptions = selectElement.AllSelectedOptions;
        foreach(IWebElement option in SelectOptions){
            options.Add(option.Text);
            }
        return options;
        }
    }
}
