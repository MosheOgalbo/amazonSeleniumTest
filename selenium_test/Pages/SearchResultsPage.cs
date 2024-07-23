using OpenQA.Selenium;
using selenium_test;
using OpenQA.Selenium.Interactions;
using DotnetSeleniumTest.Driver;

namespace DotnetSeleniumTest.Pages
{
    public class SearchResultsPage
    {
        private readonly IWebDriver? driver;
        public SearchResultsPage(IWebDriver driver){
            this.driver = driver;
        }

        // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
          By priceFilter =>By.XPath("//input[@class='s-range-input' and @type='range' and contains(@id, 'range-slider_slider-item_upper-bound-slider')]");
          By buttonPriceFilter => By.XPath("//input[@type='submit' and contains(@class, 'a-button-input')]");
          By memoryFilter => By.XPath("//span[contains(text(), '16 GB')]");
          By ratingFilter => By.XPath("//span[contains(text(),'4 Stars & Up')]");
        // החזרת IWebElement עבור Locator
        private IWebElement GetElement(By by) => driver.FindElement(by);




//הזזה אלמנט סוליד
   public  void SliderFilters(int minValue=0, int maxValue=50 ){

        // יצירת אובייקט Actions כדי לבצע פעולות עם העכבר
        OpenQA.Selenium.Interactions.Actions actions = new OpenQA.Selenium.Interactions.Actions(driver);
        // הזזת הסליידר. כאן דוגמה להזזה של 50 פיקסלים ימינה.
       actions.ClickAndHold(GetElement(priceFilter)).MoveByOffset(maxValue, minValue).Release().Perform();
       }

    // סינון תוצאות חיפוש לפי קריטריונים
        public void ApplyFilters(){
            WaitDriver webScreenWait = new WaitDriver(driver);
            webScreenWait.WebScreenWait(memoryFilter);
            GetElement(memoryFilter).ClickInElement();
            webScreenWait.WebScreenWait(priceFilter);
            SliderFilters(4,500);
            webScreenWait.WebScreenWait(priceFilter);

        }


    }
}







        // איסוף קישורים של מוצרים
        //     public List<string> CollectProductLinks()
        //     {
        //         var productLinks = new List<string>();
        //         var products = _driver.FindElements(By.CssSelector("div.s-main-slot div.s-result-item"));

        //         foreach (var product in products)
        //         {
        //             var reviews = product.FindElements(By.CssSelector(".a-section .a-spacing-none .a-size-small .a-link-normal"));
        //             if (reviews.Count > 10)
        //             {
        //                 bool badReviewFound = false;
        //                 for (int i = 0; i < 10; i++)
        //                 {
        //                     if (reviews[i].Text.ToLower().Contains("bad"))
        //                     {
        //                         badReviewFound = true;
        //                         break;
        //                     }
        //                 }

        //                 if (!badReviewFound)
        //                 {
        //                     var link = product.FindElement(By.CssSelector("h2 a")).GetAttribute("href");
        //                     productLinks.Add(link);
        //                 }
        //             }

        //             if (productLinks.Count >= 10)
        //             {
        //                 break;
        //             }
        //         }

        //         return productLinks;
        //     }
