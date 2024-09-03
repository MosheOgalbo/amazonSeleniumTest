using OpenQA.Selenium;
using selenium_test;
using OpenQA.Selenium.Interactions;
using DotnetSeleniumTest.Browser;

namespace DotnetSeleniumTest.Pages
{
    public class SearchResultsPage : Browser.DriverTest
    {
        //private readonly IWebDriver? _driver;
        private readonly WaitDriver _waitElement;
        public SearchResultsPage()
        {
            //this._driver = Browser.DriverTest.driver;
            //_waitElement= new WaitDriver(driver);
            _waitElement = new WaitDriver();
        }

        // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
        By priceFilter => By.XPath("//*[contains(@class, 'slider-container')]");
        // מגדירים את האלמנטים שצריך לאתר בדף תוצאות החיפוש
        private By lowerBoundSlider => By.Id("p_36/range-slider_slider-item_lower-bound-slider");
        private By upperBoundSlider => By.Id("p_36/range-slider_slider-item_upper-bound-slider");
        private By byMemoryFilter => By.XPath("//div//span[contains(text(),'16 GB')]");

        By ratingFilter => By.XPath("//span[contains(text(),'4 Stars & Up')]");
        By byButtonFilter => By.XPath("//*[contains(@class,'sf-submit-range-button')]");
        By byProductCard => By.XPath("//*[contains(@class,'gsx-ies-anchor')]");
        By byProductReviews => By.XPath("//*[contains(@class,'instrumentation-wrapper')]");
        // החזרת IWebElement עבור Locator
        private IWebElement GetElement(By by) => Browser.DriverTest.driver!.FindElement(by);
        private IReadOnlyList<IWebElement> GetElements(By by) => Browser.DriverTest.driver!.FindElements(by);

        public void AdjustSlider(int upperBoundValue)
        {
            // המתנה לטעינת הסליידר
            Thread.Sleep(200);
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
        public void ApplyFilters()
        {
            // ActionsInWeb actionsInWeb = new ActionsInWeb(driver);
            //WaitDriver waitElement = new WaitDriver(driver);
            // _waitElement.UnitToElementIsClick(byProductCard);
            RefreshDriver();
            _waitElement.UnitToElementIsClick(byMemoryFilter);
            // actionsInWeb.Screenshot();
            GetElement(byMemoryFilter).ClickInElement();
            _waitElement.UnitToElementIsClick(priceFilter);
            AdjustSlider(49);
            //actionsInWeb.Screenshot();
            _waitElement.UnitToElementIsClick(priceFilter);
            GetElement(byButtonFilter).ClickInElement();
            _waitElement.UnitToElementIsClick(byButtonFilter);
        }

        // איסוף קישורים של מוצרים
        public List<string> CollectProductLinks()
        {
            List<string> productLinks = new List<string>();
            IReadOnlyList<IWebElement> products = GetElements(byProductCard);

            foreach (var product in products)
            {
                IReadOnlyList<IWebElement> reviews = product.FindElements(byProductReviews);

                if (reviews.Count > 0)
                {
                    bool badReviewFound = false;

                    for (int i = 0; i < Math.Min(reviews.Count, 10); i++)
                    {
                        string reviewText = reviews[i].Text;
                        // הסר את כל התווים שאינם מספריים
                        string reviewCountStr = new string(reviewText.Where(char.IsDigit).ToArray());

                        // בדוק אם הצלחנו להמיר את הטקסט למספר שלם
                        if (int.TryParse(reviewCountStr, out int reviewCount))
                        {
                            // בדוק אם מספר הביקורות קטן מ-10 או אם יש ביקורת רעה
                            if (reviewCount < 10 || reviewText.Contains("bad", StringComparison.OrdinalIgnoreCase))
                            {
                                badReviewFound = true;
                                break;
                            }
                        }
                        else
                        {
                            // אם לא הצלחנו להמיר את הטקסט למספר, נניח שיש יותר מ-10 ביקורות
                            badReviewFound = true;
                            break;
                        }
                    }

                    // הוסף את הקישור אם לא נמצאה ביקורת רעה
                    if (!badReviewFound)
                    {
                        var link = product.FindElement(By.CssSelector("h2 a")).GetAttribute("href");
                        productLinks.Add(link);

                        // עצור את הלולאה אם נאספו 10 קישורים
                        if (productLinks.Count >= 10)
                        {
                            break;
                        }
                    }
                }
            }

            return productLinks;
        }

        public List<string> CollectProductLinksTopNen()
        {
            RefreshDriver();
            _waitElement.UnitToElementIsClick(byProductCard);

            List<string> productList = new List<string>();
            IReadOnlyList<IWebElement> products = GetElements(byProductCard);
            foreach (var product in products)
            {
                IReadOnlyList<IWebElement> reviews = product.FindElements(byProductReviews);
                if (reviews.Count > 10)
                {
                    bool badReviewFound = false;
                    for (int i = 0; i < 10; i++)
                    {    // קבל את הטקסט מהאלמנט
                        string reviewText = reviews[i].Text;
                        // הסר את כל התווים שאינם מספריים
                        //string reviewCountStr = new string(reviewText.Where(char.IsDigit).ToArray());
                        string reviewCountStr = new string(reviewText.Where(c => char.IsDigit(c) || c == ',').ToArray());

                        // הסר את הפסיקים מהטקסט
                        reviewCountStr = reviewCountStr.Replace(",", "");
                        // המר את הטקסט למספר שלם
                        int reviewCount = int.Parse(reviewCountStr);

                        if (reviewCount < 10)
                        {
                            if (reviewText.Contains("bad"))
                            {
                                badReviewFound = true;
                                break;
                            }
                            badReviewFound = true;
                            break;
                        }
                    }

                    if (!badReviewFound)
                    {
                        var linkItem = product.FindElement(By.CssSelector("a")).GetAttribute("href");
                        // string priceItem = product.FindElement(By.CssSelector(".a-price-whole")).Text;
                        // string priceWhole = product.FindElement(By.XPath(".//span[@class='a-price-whole']")).Text;
                        // string priceFraction = product.FindElement(By.XPath(".//span[@class='a-price-fraction']")).Text;
                        // string fullPriceString = priceWhole + "." + priceFraction;
                        // float fullPrice = float.Parse(fullPriceString);
                        float? fullPrice = CheckValidPrice(product);
                        string descriptionItem = product.FindElement(By.XPath(".//span[contains(@class,'base a-text-nor')]")).Text;
                        // productList.Add(new ProductInfo
                        // {
                        //     URL = linkItem,
                        //     Price = fullPrice,
                        //     Manufacturer = descriptionItem
                        // }
                        // );

                        productList.Add(linkItem);

                        // עצור את הלולאה אם נאספו 10 קישורים
                        if (productList.Count >= 10)
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return productList;
        }
        public float? CheckValidPrice(IWebElement product)
        {
            try
            {
                // נסה למצוא את האלמנטים של המחיר השלם והחלק העשרוני
                var priceWholeElement = product.FindElement(By.XPath(".//span[@class='a-price-whole']"));
                var priceFractionElement = product.FindElement(By.XPath(".//span[@class='a-price-fraction']"));

                // קבל את הטקסט של המחיר השלם והחלק העשרוני
                string priceWhole = priceWholeElement.Text;
                string priceFraction = priceFractionElement.Text;

                // חיבור המחיר השלם והחלק העשרוני למחרוזת אחת
                string fullPriceString = $"{priceWhole}.{priceFraction}";

                // המרת המחרוזת למספר עשרוני
                if (float.TryParse(fullPriceString, out float fullPrice))
                {
                    return fullPrice;
                }
                else
                {
                    // אם לא ניתן להמיר למספר עשרוני, החזר null
                    return null;
                }
            }
            catch (NoSuchElementException)
            {
                // במקרה שהאלמנט לא נמצא, החזר null
                return null;
            }
            catch (FormatException)
            {
                // במקרה של בעיית עיצוב, החזר null
                return null;
            }
        }


    }


}
