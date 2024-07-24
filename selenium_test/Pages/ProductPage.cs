using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonAutomation.Pages
{
    public class ProductPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        // אתחול של דף המוצר
        public ProductPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        // פונקציה לבדוק אם המוצר תקין
        public bool IsValidProduct()
        {
            var reviewsElement = _driver.FindElement(By.Id("acrCustomerReviewText"));
            int reviewCount = int.Parse(reviewsElement.Text.Split(' ')[0].Replace(",", ""));
            if (reviewCount < 10) return false; // בודק אם למוצר יש לפחות 10 ביקורות

            var reviews = _driver.FindElements(By.CssSelector(".review-text-content span"));
            return !reviews.Take(10).Any(review => review.Text.Contains("bad", StringComparison.OrdinalIgnoreCase));
            // בודק אם אחת מהביקורות הראשונות מכילה את המילה "bad"
        }
    }
}
