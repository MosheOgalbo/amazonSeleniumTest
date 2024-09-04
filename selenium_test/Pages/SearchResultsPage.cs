using OpenQA.Selenium;
using selenium_test;
using OpenQA.Selenium.Interactions;
using DotnetSeleniumTest.Browser;

namespace DotnetSeleniumTest.Pages
{
    public class SearchResultsPage : DriverTest
    {
        private readonly WaitDriver _waitElement;
        public SearchResultsPage()
        {
            _waitElement = new WaitDriver();
        }

        // Define the elements that need to be found on the search results page

        // Define the elements that need to be found on the search results page
        private By priceFilter => By.XPath("//*[contains(@class, 'slider-container')]");
        private By lowerBoundSlider => By.Id("p_36/range-slider_slider-item_lower-bound-slider");
        private By upperBoundSlider => By.Id("p_36/range-slider_slider-item_upper-bound-slider");
        private By byMemoryFilter => By.XPath("//div//span[contains(text(),'16 GB')]");
        private By ratingFilter => By.XPath("//span[contains(text(),'4 Stars & Up')]");
        private By byButtonFilter => By.XPath("//*[contains(@class,'sf-submit-range-button')]");
        private By byProductCard => By.XPath("//*[contains(@class,'gsx-ies-anchor')]");
        private By byProductReviews => By.XPath("//*[contains(@class,'instrumentation-wrapper')]");
        private By byProductDescription => By.XPath(".//span[contains(@class,'base a-text-nor')]");
        private By byPriceWhole => By.XPath(".//span[@class='a-price-whole']");
        private By byPriceFraction => By.XPath(".//span[@class='a-price-fraction']");

        private IWebElement GetElement(By by) => driver!.FindElement(by);
        private IReadOnlyList<IWebElement> GetElements(By by) => driver!.FindElements(by);

        public void AdjustSlider(int upperBoundValue)
        {
            // Waiting for the slider to load
            Thread.Sleep(200);
            // Getting the slider element
            IWebElement slider = GetElement(upperBoundSlider);
            // Getting the width of the slider
            int sliderWidth = slider.Size.Width;
            // Calculate the position of the move
            double percentage = upperBoundValue / 100.0;
            int xOffset = (int)(sliderWidth * percentage);
            // Create an Actions object to perform actions with the mouse
            Actions actions = new Actions(driver);
            // Click and Hold the slider, Move, and Release
            actions.ClickAndHold(slider)
                   .MoveByOffset(xOffset - sliderWidth, 0) // תחילת ה-Move ממיקום מרכז הסליידר הנוכחי
                   .Release()
                   .Perform();
            // Waiting to view the result
            Thread.Sleep(5000);
        }

        /* filter search results according to criteria */
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

        // Collect product links
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
                        // Remove all non-number characters
                        string reviewCountStr = new string(reviewText.Where(char.IsDigit).ToArray());

                        // Check if we were able to convert the text to an integer
                        if (int.TryParse(reviewCountStr, out int reviewCount))
                        {
                            // Check if the number of reviews is less than 10 or if there is a bad review
                            if (reviewCount < 10 || reviewText.Contains("bad", StringComparison.OrdinalIgnoreCase))
                            {
                                badReviewFound = true;
                                break;
                            }
                        }
                        else
                        {
                            // If we failed to convert the text to a number, assume there are more than 10 reviews
                            badReviewFound = true;
                            break;
                        }
                    }

                    // add the link if no bad review found
                    if (!badReviewFound)
                    {
                        var link = product.FindElement(By.CssSelector("h2 a")).GetAttribute("href");
                        productLinks.Add(link);
                        // Stop the loop if 10 links have been collected
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
                    {
                        // Get the text from the element
                        string reviewText = reviews[i].Text;
                        // Remove all non-number characters
                        string reviewCountStr = new string(reviewText.Where(c => char.IsDigit(c) || c == ',').ToArray());

                        // Remove the commas from the text
                        reviewCountStr = reviewCountStr.Replace(",", "");
                        // Convert the text to an integer
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
                        string linkItem = product.FindElement(By.CssSelector("a")).GetAttribute("href");
                        float? fullPrice = CheckValidPrice(product);
                        string descriptionItem = product.FindElement(byProductDescription).Text;
                        // productList.Add(new ProductInfo
                        // {
                        //     URL = linkItem,
                        //     Price = fullPrice,
                        //     Manufacturer = descriptionItem
                        // }
                        // );
                        productList.Add(linkItem);

                        // Stop the loop if 10 links have been collected
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
                _waitElement.UnitToElementIsClick(byPriceWhole);
                // Try to find the elements of the whole price and the decimal part
                IWebElement priceWholeElement = product.FindElement(byPriceWhole);
                IWebElement priceFractionElement = product.FindElement(byPriceFraction);

                string priceWhole = priceWholeElement.Text;
                string priceFraction = priceFractionElement.Text;
                // Concatenation of the whole price and the decimal part into one string
                string fullPriceString = $"{priceWhole}.{priceFraction}";

                // Convert string to decimal number
                if (float.TryParse(fullPriceString, out float fullPrice))
                {
                    return fullPrice;
                }
                else
                {
                    return null;
                }
            }
            catch (NoSuchElementException)
            {
                // In case the widow is not found, return null
                return null;
            }
            catch (FormatException)
            {
                // In case of formatting problem, return null
                return null;
            }
        }
    }
}
