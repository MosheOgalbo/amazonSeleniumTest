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
          By priceFilter => By.XPath("//*[contains(@class, 'slider-container')]");
          // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
         private By lowerBoundSlider => By.Id("p_36/range-slider_slider-item_lower-bound-slider");
         private By upperBoundSlider => By.Id("p_36/range-slider_slider-item_upper-bound-slider");
          By memoryFilter => By.XPath("//span[contains(text(), '16 GB')]");
          By ratingFilter => By.XPath("//span[contains(text(),'4 Stars & Up')]");
          By byButtonFilter => By.XPath("//*[contains(@class,'sf-submit-range-button')]");
        // החזרת IWebElement עבור Locator
        private IWebElement GetElement(By by) => driver.FindElement(by);

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

        // איסוף קישורים של מוצרים
        public List<string> CollectProductLinks()
        {
            var productLinks = new List<string>();
            var products = driver.FindElements(By.CssSelector("div.s-main-slot div.s-result-item"));

            foreach (var product in products)
            {
                try
                {
                    // בדיקת מחיר
                    var priceElement = product.FindElement(By.CssSelector(".a-price-whole"));
                    var priceText = priceElement.Text.Replace(",", "");
                    var price = Convert.ToDecimal(priceText);

                    if (price > 500)
                    {
                        continue; // דילוג על מוצרים מעל $500
                    }

                    // בדיקת זיכרון
                    var memoryElement = product.FindElements(By.XPath(".//*[contains(text(), '16 GB')]"));
                    if (memoryElement.Count == 0)
                    {
                        continue; // דילוג על מוצרים ללא 16GB זיכרון
                    }

                    // בדיקת דירוג
                    var ratingElement = product.FindElements(By.CssSelector(".a-icon-alt"));
                    if (ratingElement.Count == 0)
                    {
                        continue; // דילוג על מוצרים ללא דירוג
                    }
                    var ratingText = ratingElement[0].Text;
                    var rating = Convert.ToDecimal(ratingText.Split(' ')[0].Replace(",", "."));

                    if (rating < 4)
                    {
                        continue; // דילוג על מוצרים עם דירוג פחות מ-4 כוכבים
                    }

                    // הוספת הקישור למוצר
                    var linkElement = product.FindElement(By.CssSelector("h2 a"));
                    var link = linkElement.GetAttribute("href");
                    productLinks.Add(link);
                }
                catch (NoSuchElementException)
                {
                    // התעלמות ממוצרים שאין להם את אחד האלמנטים הנדרשים
                    continue;
                }

                if (productLinks.Count >= 10)
                {
                    break; // הפסקת החיפוש אם יש יותר מ-10 תוצאות
                }
            }

            return productLinks;
        }
        }

}
