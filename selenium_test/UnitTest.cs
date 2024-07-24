using NUnit.Framework;
using OpenQA.Selenium;
using DotnetSeleniumTest.Pages;
using  DotnetSeleniumTest.Driver;
using AmazonAutomation.Services;

namespace selenium_test;

[TestFixture("laptop")]
public class UnitTest{
    private IWebDriver _driver;
    private ActionsInWeb _actions;
    private readonly string itemSearch;


    public UnitTest(string _itemSearch){
        this.itemSearch = _itemSearch;
       }

        [OneTimeSetUp]
        public void OneTimeSetUp(){
            _driver = Driver.Initialize("https://www.amazon.com/");
            _actions = new ActionsInWeb(_driver);
        }


    [SetUp]
    public void Setup(){
        Console.WriteLine("nwe Setup");
    }

    [Test]
    [Category("FirstTest")]
     public void FirstTest(){
        HomePage homesPage = new HomePage(_driver);
        homesPage.SearchForItem(itemSearch);
        _actions.Screenshot();
       }
       [Test]
       public void SecondTest(){
           //שמירה על הפרטים לפי הדרישה
           SearchResultsPage  searchResultsPage = new SearchResultsPage(_driver);
           searchResultsPage.ApplyFilters();
           var productLinks = searchResultsPage.CollectProductLinks();
        //    Console.WriteLine(productLinks.Count);
           FileService fileService = new FileService();
           fileService.SaveLinksToJsonFile(productLinks,"../../../TestLinks.json");
       }

    [TearDown]
    public void DownTest() {
        _actions.Screenshot();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown(){
        Driver.Cleanup(_driver);
        // _driver.Quit();
        // _driver.Dispose();
    }
}
