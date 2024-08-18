using NUnit.Framework;
using OpenQA.Selenium;
using DotnetSeleniumTest.Driver;

namespace selenium_test;

//[SetUpFixture]
public class RunningTest{

    //public IWebDriver _driver;
    public ActionsInWeb _actions;

     [OneTimeSetUp]
     public void GlobalSetup(){
        Driver.Initialize("https://www.amazon.com/");
        _actions = new ActionsInWeb();

        }

        // [Test]
        // public void RunUnitTest1()
        // {
        //     new TestMain("laptop");

        // }
         [OneTimeTearDown]
        public void GlobalTeardown(){
           Driver.Cleanup();

        }

}
