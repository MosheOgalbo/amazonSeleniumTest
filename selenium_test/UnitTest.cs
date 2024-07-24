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
    private readonly string _itemSearch;
    private  List<string>  _productLinks ;

    public UnitTest(string _itemSearch){
        this._itemSearch = _itemSearch;
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
        homesPage.SearchForItem(_itemSearch);
        _actions.Screenshot();
       }
       [Test]
       public void SecondTest(){
           //שמירה על הפרטים לפי הדרישה
           SearchResultsPage  searchResultsPage = new SearchResultsPage(_driver);
           searchResultsPage.ApplyFilters();
           //מחזיר 10 פרטים
            _productLinks = searchResultsPage.CollectProductLinksTopNen();
           FileService fileService = new FileService();
           fileService.SaveLinksToJsonFile(_productLinks,"../../../TestLinks.json");
       }
       [Test]
       public void ThirdTest(){
// ודא שהרשימה אינה ריקה ויש לפחות שני פריטים
if (_productLinks.Count > 1)
{
    Driver.TransitionBrowser(_driver, _productLinks[1]);
            _actions.Screenshot();

}
else
{
    Console.WriteLine("לא נמצאו קישורים מספיקים ברשימה.");
}
       }
    [TearDown]
    public void DownTest() {
        _actions.Screenshot();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown(){
        Driver.Cleanup(_driver);
    }
}
