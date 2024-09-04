using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test.Services;
using DotnetSeleniumTest.Browser;

namespace DotnetSeleniumTest.Pages
{
    public class ProductPage : DriverTest
    {
        private readonly IWebDriver? _driver;
        private readonly WaitDriver _wait;

        public ProductPage()
        {
            _driver = driver;
            _wait = new WaitDriver();
        }

        private By byCustomerReviews => By.XPath("//*[contains(@id,'customer_review')]");
        private By byReviewerName => By.XPath("//span [@class='a-profile-name']");
        private By byReviewText => By.XPath("//div[contains(@class,'reviewText')]");
        private By byAddProductCartButton => By.Id("add-to-cart-button");
        private By byProductCartIcon => By.Id("nav-cart-count-container");
        private IWebElement GetElement(By by) => _driver!.FindElement(by);
        private IReadOnlyList<IWebElement> GetElements(By by) => _driver!.FindElements(by);

        public void NavigateProductPage(List<string> ProducList)
        {
            if (DataCheck.IsDataEmpty(ProducList))
            {
                TransitionBrowser(ProducList[0]);
            }
        }

        // Takes all reviews of the product
        public List<ItemReviewModel> GetAllReviews()
        {
            var reviews = new List<ItemReviewModel>();

            IReadOnlyList<IWebElement> reviewsElement = GetElements(byCustomerReviews);
            foreach (IWebElement review in reviewsElement)
            {
                IWebElement reviewerNameElement = review.FindElement(byReviewerName);
                IWebElement reviewTextElement = review.FindElement(byReviewText);

                reviews.Add(new ItemReviewModel
                {
                    ReviewerName = reviewerNameElement.Text,
                    ReviewText = reviewTextElement.Text,
                }
                );
            }
            return reviews;

        }

        //Go to the add this product screen
        public void AddProductToCart()
        {
            _wait.UnitToElementIsClick(byAddProductCartButton)?.Click();
            _wait.UntilElementIsRemoved(byAddProductCartButton);
            _wait.UnitToElementIsClick(byProductCartIcon)?.Click();
        }
    }
}
