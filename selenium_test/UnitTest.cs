using NUnit.Framework;
using OpenQA.Selenium;
using DotnetSeleniumTest.Pages;
using  DotnetSeleniumTest.Driver;

namespace selenium_test;

[TestFixture("laptop")]
public class UnitTest{
    private IWebDriver? _driver;
    private   ActionsInWeb actions;
    private readonly string itemSearch;


    public UnitTest(string itemSearch){
       this.itemSearch = itemSearch;
       }

    [SetUp]
    public void Setup(){
         _driver = Driver.Initialize("https://www.amazon.com/-/he/");
         actions = new ActionsInWeb(_driver);
    }
    // [Test]
    // public void TestWithPOM( ) {
    //     LoginPage loginPage = new LoginPage(_driver);
    //     loginPage.Login(username, password);

    // }

    [Test]
    [Category("FirstTest")]
     public void FirstTest(){
        HomePage homesPage = new HomePage(_driver);
        homesPage.SearchForItem(itemSearch);
        actions.Screenshot();

       SearchResultsPage  searchResultsPage = new SearchResultsPage(_driver);
       searchResultsPage.ApplyFilters();
       }

    [TearDown]
    public void DownTest() {
        actions.Screenshot();
        _driver.Quit();
        // _driver.Dispose();
    }
}




//     [Test]
 //  [Order(1)]
    // [Category("FirstTest")]
//     [TestCaseSource(nameof(Login))]
//     public void TestPOM(LoginModel  longModel) {
//         LoginPage loginPage = new LoginPage(_driver);
//         loginPage.Login(longModel.Username, longModel.Password);

//     }
// public static IEnumerable<LoginModel> Login() {
//     yield return new LoginModel(){
//         username = "admin",
//         password = "password"
//     };

// }
