using NUnit.Framework;
using DotnetSeleniumTest.Driver;
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
        Driver.Initialize("https://www.amazon.com/");
        _actions = new ActionsInWeb();
        _extent = new ExtentReports();
        var spark = new ExtentSparkReporter("Spark.html");
        _extent.AttachReporter(spark);




    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        Driver.Cleanup();

        // try{
        //     _extent.Flush();
        //     }
        // catch (Exception ex){
        //     Console.WriteLine(ex.ToString());
        //     }
    }
}
