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
        ProductPage productPage = new ProductPage(_driver);
        FileService fileService = new FileService();
        productPage.NavigateProductPage(_productLinks);
        List<DotnetSeleniumTest.ReviewModel> reviewModels = productPage.GetAllReviews();
        fileService.SaveReviewsToJsonFile(reviewModels,"../../../TestLinks.json");

        // _actions.Screenshot();
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
