﻿using NUnit.Framework;
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
    FileService fileService;

      public UnitTest(string _itemSearch){
        this._itemSearch = _itemSearch;
       }
       [OneTimeSetUp]
       public void OneTimeSetUp(){
            _driver = Driver.Initialize("https://www.amazon.com/");
            _actions = new ActionsInWeb();
        }

       [SetUp]
       public void Setup(){
        Console.WriteLine("nwe Setup");
         fileService = new FileService();
        }

       [Test]
       [Category("FirstTest")]
       [Order(1)]
       public void FirstTest(){
        HomePage homesPage = new HomePage(_driver);
        homesPage.SearchForItem(_itemSearch);
        //_actions.Screenshot();
       }

       [Test]
       [Order(2)]
       public void SecondTest(){
           //שמירה על הפרטים לפי הדרישה
           SearchResultsPage  searchResultsPage = new SearchResultsPage(_driver);
           searchResultsPage.ApplyFilters();
           //מחזיר 10 פרטים
            _productLinks = searchResultsPage.CollectProductLinksTopNen();
           fileService.SaveLinksToJsonFile(_productLinks,"../../../TestLinks.json");
       }

       [Test]
       [Order(3)]
       public void ThirdTest(){
        ProductPage productPage = new ProductPage(_driver);
        productPage.NavigateProductPage(_productLinks);
        List<DotnetSeleniumTest.ItemReviewModel> reviewModels = productPage.GetAllReviews();
        fileService.SaveReviewsToJsonFile(reviewModels,"../../../TestReviews.json");
       }

       [Test]
       [Order(4)]
       public void FourthTest(){
         ProductPage productPage = new ProductPage(_driver);
         productPage.AddProductToCart();
         CartPage cartPage = new CartPage(_driver);
         cartPage.ProceedToPayForProduct();
       }

       [Test]
       [Order(5)]
       public void FifthTest(){
        CheckoutPage checkoutPage = new CheckoutPage(_driver);
        checkoutPage.MakingPaymentForProduct();
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
