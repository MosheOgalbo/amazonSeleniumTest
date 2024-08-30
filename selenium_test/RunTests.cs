using NUnit.Framework;
using DotnetSeleniumTest.Browser;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.MarkupUtils;



namespace selenium_test;

//[SetUpFixture]
public class RunningTest
{
    //public IWebDriver _driver;
    protected ExtentReports? _extent;
    protected ExtentTest? _test;
    public ActionsInWeb? _actions;


    [OneTimeSetUp]
    public void GlobalSetup()
    {
        DriverTest.Initialize("https://www.amazon.com/");
        _actions = new ActionsInWeb();

        var htmlReporter = new ExtentSparkReporter("../ExtentReport.html");
        _extent = new ExtentReports();
        _extent.AttachReporter(htmlReporter);




    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        DriverTest.Cleanup();
        _extent?.Flush();

    }
}
