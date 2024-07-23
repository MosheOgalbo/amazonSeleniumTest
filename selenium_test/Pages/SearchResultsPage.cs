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
          // By priceFilter =>By.XPath("//input[@class='s-range-input' and @type='range' and contains(@id, 'range-slider_slider-item_upper-bound-slider')]");
          By priceFilter => By.XPath("//*[contains(@class, 'slider-container')]");
          // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
         private By lowerBoundSlider => By.Id("p_36/range-slider_slider-item_lower-bound-slider");
         private By upperBoundSlider => By.Id("p_36/range-slider_slider-item_upper-bound-slider");

          By memoryFilter => By.XPath("//span[contains(text(), '16 GB')]");
          By ratingFilter => By.XPath("//span[contains(text(),'4 Stars & Up')]");
          By byButtonFilter => By.XPath("//*[contains(@class,'sf-submit-range-button')]");
        // החזרת IWebElement עבור Locator
        private IWebElement GetElement(By by) => driver.FindElement(by);

        // הזזה אלמנט סוליד
        // public void SliderFilters(int lowerValue = 20, int upperValue = 90)
        // {// אתחול Actions class להזזת הסליידר
        //     Actions actions = new Actions(driver);

        //     // הזזת ה-slider התחתון
        //     // IWebElement lowerSlider = GetElement(lowerBoundSlider);
        //     // actions.ClickAndHold(lowerSlider)
        //     //        .MoveByOffset(lowerValue, 0) // להזיז את ה-slider התחתון
        //     //        .Release()
        //     //        .Perform();

        //     // הזזת ה-slider העליון
        //     IWebElement upperSlider = GetElement(upperBoundSlider);
        //     actions.ClickAndHold(upperSlider)
        //            .MoveByOffset(upperValue, 0) // להזיז את ה-slider העליון
        //            .Release()
        //            .Perform();

        //     // המתנה לצפייה בתוצאה
        // }

 public void AdjustSlider(int upperBoundValue)
        {
            // המתנה לטעינת הסליידר
            Thread.Sleep(2000);

            // קבלת אלמנט הסליידר
            IWebElement slider = GetElement(upperBoundSlider);

            // קבלת רוחב הסליידר
            int sliderWidth = slider.Size.Width;

            // חישוב מיקום ההזזה
            double percentage = upperBoundValue / 100.0;
            int xOffset = (int)(sliderWidth * percentage);

            // יצירת אובייקט Actions כדי לבצע פעולות עם העכבר
            Actions actions = new Actions(driver);

            // Click and Hold the slider, Move, and Release
            actions.ClickAndHold(slider)
                   .MoveByOffset(xOffset - sliderWidth, 0) // תחילת ה-Move ממיקום מרכז הסליידר הנוכחי
                   .Release()
                   .Perform();

            // המתנה לצפייה בתוצאה
            Thread.Sleep(5000);
        }


    // סינון תוצאות חיפוש לפי קריטריונים
        public void ApplyFilters(){
            ActionsInWeb actionsInWeb = new ActionsInWeb(driver);
            WaitDriver webScreenWait = new WaitDriver(driver);
            webScreenWait.WebScreenWait(memoryFilter);
            actionsInWeb.Screenshot();
            GetElement(memoryFilter).ClickInElement();
            webScreenWait.WebScreenWait(priceFilter);
            AdjustSlider(49);
            actionsInWeb.Screenshot();
            webScreenWait.WebScreenWait(priceFilter);
            GetElement(byButtonFilter).ClickInElement();
            webScreenWait.WebScreenWait(byButtonFilter);
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
