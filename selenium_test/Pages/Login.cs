using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using selenium_test;


namespace DotnetSeleniumTest.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        public  LoginPage(IWebDriver driver){
            this.driver = driver;
        }
        IWebElement ByLoginLink => driver.FindElement(By.LinkText("Login"));
        IWebElement ByUserName => driver.FindElement(By.Name("UserName"));
        IWebElement ByPassword => driver.FindElement(By.Name("Password"));
        IWebElement BySignInButton => driver.FindElement(By.XPath("//input[@type='submit']"));

        public void ClickLoginLink(){
           // ByLoginLink.Click();
            ByLoginLink.ClickInElement();
        }
        public void EnterUserName(string username){
           // ByUserName.SendKeys(username);
           ByUserName.EnterText(username);

        }
        public void EnterPassword(string password){
           // Password.SendKeys(password);
           ByPassword.EnterText(password);

        }
        public void ClickSignInButton(){
            //
            //BySignInButton.Click();
              BySignInButton.ClickInElement();
        }

        public void Login(string username, string password){
            ClickLoginLink();
            EnterUserName(username);
            EnterPassword(password);
            ClickSignInButton();
        }
    }
}
