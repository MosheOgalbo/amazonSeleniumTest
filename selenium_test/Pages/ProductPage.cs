using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using AmazonAutomation.Services;
using DotnetSeleniumTest.Driver;

namespace DotnetSeleniumTest.Pages
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
      By byCustomerReviews => By.XPath("//*[contains(@id,'customer_review')]");
      private IWebElement GetElement(By by) => _driver.FindElement(by);
      private IReadOnlyList <IWebElement> GetElements(By by) => _driver.FindElements(by);


        public  void NavigateProductPage(List<string> ProducList){
            if(DataCheck.IsDataEmpty(ProducList)){
                Driver.Driver.TransitionBrowser(_driver, ProducList[0]);
             }
        }
        // לוקח את כול הביקורות של המוצר
        public List<ReviewModel> GetAllReviews()
        {// איסוף כל הביקורות

            var reviews = new List<ReviewModel>();

            IReadOnlyList<IWebElement> reviewsElement = GetElements(byCustomerReviews);
            foreach (var review in reviewsElement){
                IWebElement reviewerNameElement = review.FindElement(By.CssSelector(".a-profile-name"));
                IWebElement reviewTextElement = review.FindElement(By.CssSelector("a-expander-content reviewText review-text-content a-expander-partial-collapse-content"));
                 // מציאת שם המבקר
                 string reviewerName = reviewerNameElement.Text;
            // מציאת טקסט הביקורת
            string reviewText = reviewTextElement.Text;
            reviews.Add(new ReviewModel
                {
                    ReviewerName = reviewerName,
                    ReviewText = reviewText
                });
            }
            return reviews;

        }
    }
}
