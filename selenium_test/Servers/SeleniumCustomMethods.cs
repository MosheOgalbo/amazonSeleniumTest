using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace selenium_test
{
    public static class SeleniumCustomMethods
    {
        public static void ClickInElement(this IWebElement locator){
            try{
                locator.Click();
            }catch(Exception e){
                Console.WriteLine(e.Data.ToString());
            }
        }

        public static void EnterText(this IWebElement  locator ,string text){
            try{
                locator.Clear();
           locator.SendKeys(text);
           }catch(Exception e){
                Console.WriteLine(e.Data.ToString());
            }
        }
        public static void SelectDropdownByText(this IWebElement locator, string text){
            try{
                SelectElement selectElement= new SelectElement(locator);
                selectElement.SelectByText(text);
                }catch(Exception e){
                Console.WriteLine(e.Data.ToString());
                }
        }
        public static void SelectDropdownByValue(this IWebElement locator, string value){
                try{
                    SelectElement selectElement= new SelectElement(locator);
                    selectElement.SelectByValue(value);
                }catch(Exception e){
                    Console.WriteLine(e.Data.ToString());
                }
        }
        public static void MultiSelectElements(this IWebElement locator, string[] values){
            try{
                SelectElement multiSelect= new SelectElement(locator);
                foreach(var value in values){
                    multiSelect.SelectByValue(value);
                    }
             }catch(Exception e){
                Console.WriteLine(e.Data.ToString());
           }
        }
       public static List<string> GetAllSelectedOptions(this IWebElement locator){
        if (locator == null){
            throw new ArgumentNullException(nameof(locator), "The locator cannot be null.");
            }
        List<string> options = new List<string>();
        SelectElement selectElement = new SelectElement(locator);
        try{
            IList<IWebElement> selectedOptions = selectElement.AllSelectedOptions;
            foreach (IWebElement option in selectedOptions){
                options.Add(option.Text);
            }
        }catch (Exception e){
        throw new InvalidOperationException("Failed to get all selected options.", e);
        }
        return options;
        }

    }
}
