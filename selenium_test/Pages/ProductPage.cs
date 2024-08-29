using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test.Services;
using DotnetSeleniumTest.Driver;

namespace DotnetSeleniumTest.Pages
{
    public class ProductPage : Driver.Driver
    {
        //private readonly IWebDriver? _driver;
        private readonly WaitDriver _wait;
        // אתחול של דף המוצר
        public ProductPage()
        {
            //_driver = Driver.Driver._driver;
            //_wait = new WaitDriver(_driver);
            _wait = new WaitDriver();
        }
        private By byCustomerReviews => By.XPath("//*[contains(@id,'customer_review')]");
        private By byReviewerName => By.XPath("//span [@class='a-profile-name']");
        private By byReviewText => By.XPath("//div[contains(@class,'reviewText')]");
        private By byAddProductCartButton => By.Id("add-to-cart-button");
        private By byProductCartIcon => By.Id("nav-cart-count-container");
        private IWebElement GetElement(By by) => driver.FindElement(by);
        private IReadOnlyList<IWebElement> GetElements(By by) => driver.FindElements(by);


        public void NavigateProductPage(List<string> ProducList)
        {
            if (DataCheck.IsDataEmpty(ProducList))
            {
                Driver.Driver.TransitionBrowser(driver, ProducList[0]);
            }
        }
        // לוקח את כול הביקורות של המוצר
        public List<ItemReviewModel> GetAllReviews()
        {// איסוף כל הביקורות

            var reviews = new List<ItemReviewModel>();

            IReadOnlyList<IWebElement> reviewsElement = GetElements(byCustomerReviews);
            foreach (IWebElement review in reviewsElement)
            {
                IWebElement reviewerNameElement = review.FindElement(byReviewerName);
                IWebElement reviewTextElement = review.FindElement(byReviewText);
                // מציאת שם המבקר
                //      string reviewerName = reviewerNameElement.Text;
                // // מציאת טקסט הביקורת
                // string reviewText = reviewTextElement.Text;
                reviews.Add(new ItemReviewModel
                {
                    ReviewerName = reviewerNameElement.Text,
                    ReviewText = reviewTextElement.Text,
                }
                    );
            }
            return reviews;

        }

        //מעבר למסך הוספה מוצר זה
        public void AddProductToCart()
        {
            _wait.UnitToElementIsClick(byAddProductCartButton).Click();
            _wait.UntilElementIsRemoved(byAddProductCartButton);
            _wait.UnitToElementIsClick(byProductCartIcon).Click();
        }
    }
}
