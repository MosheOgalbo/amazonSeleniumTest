using DotnetSeleniumTest;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using DotnetSeleniumTest.Pages;
using System;
using System.IO;
// using System.Drawing.Imaging;
// using System.Reflection;
using  DotnetSeleniumTest.Driver;


namespace selenium_test;

[TestFixture("admin", "password")]
// [TestFixture("admin1", "password")]
// [TestFixture("admin2", "password")]

public class UnitTest{
    private IWebDriver? _driver;
    private readonly string username;
    private readonly string password;

    public UnitTest(string username, string password){
       this.username = username;
        this.password = password;
    }
    [SetUp]
    public void Setup(){
         _driver = Driver.Initialize("https://www.amazon.com/-/he/");
    }
    // [Test]
    // public void TestWithPOM( ) {
    //     LoginPage loginPage = new LoginPage(_driver);
    //     loginPage.Login(username, password);

    // }
    [Test]
    // [Order(1)]
    [Category("FirstTest")]
     public void FirstTest(){
        Actions actions = new Actions(_driver);
        HomePage homesPage = new HomePage(_driver);
        homesPage.SearchForItem("laptop");

        actions.Screenshot();

        }

    [TearDown]
    public void DownTest() {
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
