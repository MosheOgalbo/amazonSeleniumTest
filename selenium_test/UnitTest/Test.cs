using NUnit.Framework;
using DotnetSeleniumTest.Pages;
using AmazonAutomation.Services;

namespace selenium_test.Tests{

 [TestFixture("laptop")]
public class TestMain:RunningTest{

    FileService _fileService;
    private readonly string _itemSearch;
    private  List<string>  _productLinks ;


      public TestMain(string _itemSearch){
        this._itemSearch = _itemSearch;
       }
    [OneTimeSetUp]
       public void OneTimeSetUp(){
          _fileService = new FileService();
        }

       [SetUp]
       public void Setup(){
        Console.WriteLine("nwe Setup");
        _actions.Screenshot();
        }

       [Test]
       [Category("FirstTest")]
       [Order(1)]
       public void FirstTest(){
        HomePage homesPage = new HomePage();
        homesPage.SearchForItem(_itemSearch);
       }

       [Test]
       [Order(2)]
       public void SecondTest(){
           //שמירה על הפרטים לפי הדרישה
           SearchResultsPage  searchResultsPage = new SearchResultsPage();
           searchResultsPage.ApplyFilters();
           //מחזיר 10 פרטים
            _productLinks = searchResultsPage.CollectProductLinksTopNen();
           _fileService.SaveLinksToJsonFile(_productLinks,"../../../TestLinks.json");
       }

       [Test]
       [Order(3)]
       public void ThirdTest(){
        ProductPage productPage = new ProductPage();
        productPage.NavigateProductPage(_productLinks);
        List<DotnetSeleniumTest.ItemReviewModel> reviewModels = productPage.GetAllReviews();
        _fileService.SaveReviewsToJsonFile(reviewModels,"../../../TestReviews.json");
       }

       [Test]
       [Order(4)]
       public void FourthTest(){
         ProductPage productPage = new ProductPage();
         productPage.AddProductToCart();
         CartPage cartPage = new CartPage();
         cartPage.ProceedToPayForProduct();
       }

       [Test]
       [Order(5)]
       public void FifthTest(){
        CheckoutPage checkoutPage = new CheckoutPage();
        checkoutPage.MakingPaymentForProduct();
       }

    [TearDown]
    public void DownTest() {
        _actions.Screenshot();
    }
        [OneTimeTearDown]
      public void OneTimeTearDown(){
        _actions.Screenshot();
    }
}
}
