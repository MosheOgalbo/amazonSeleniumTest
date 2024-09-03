using NUnit.Framework;
using DotnetSeleniumTest.Browser;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.MarkupUtils;
using System.Net;
using selenium_test.Services;


namespace selenium_test;

//[SetUpFixture]
public class RunningTest : ExtentReporting
{
    //public IWebDriver _driver;
    protected ExtentReports? _extent;
    protected ExtentTest? _test;
    public ActionsInWeb? _actions;


    [OneTimeSetUp]
    public void GlobalSetup()
    {
        _actions = new ActionsInWeb();
        DriverTest.Initialize("https://www.amazon.com/");

        // var htmlReporter = new ExtentSparkReporter("../../../ReportTest/ExtentReport.html");
        // _extent = new ExtentReports();
        // _extent.AttachReporter(htmlReporter);

        // Add system info to the report
        // _extent.AddSystemInfo("Operating System", Environment.OSVersion.ToString());
        // _extent.AddSystemInfo("Host Name", Dns.GetHostName());
        // _extent.AddSystemInfo("Environment", "QA");
        // _extent.AddSystemInfo("User Name", Environment.UserName);


    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        DriverTest.Cleanup();


    }
}
