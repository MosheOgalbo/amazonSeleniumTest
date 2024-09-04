using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework.Interfaces;
using AventStack.ExtentReports.MarkupUtils;
using System.Reflection;
using NUnit.Framework;
using DotnetSeleniumTest.Browser;
using System.IO;

namespace selenium_test.Services
{

    public class ExtentReporting : DriverTest
    {
        private static ExtentReports? _extentReport;
        private static ExtentTest? _extentTest;
        ActionsInWeb actionsInWeb = new ActionsInWeb();

        public static ExtentReports startReporting()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "../../../ReportTest";
            //var path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "../../../ReportTest");
            Console.WriteLine("Report will be saved to: " + path);

            if (_extentReport == null)
            {
                Directory.CreateDirectory(path);
                _extentReport = new ExtentReports();
                var sparkReporter = new ExtentSparkReporter(path);
                _extentReport.AttachReporter(sparkReporter);

            }
            return _extentReport;
        }

        public static void CreateTest(string testName)
        {
            _extentTest = startReporting().CreateTest(testName);
        }

        public static void EndReporting()
        {
            if (_extentReport != null)
            {
                startReporting().Flush();
            }
            else
            {
                throw new InvalidOperationException("ExtentReport is not initialized. Cannot flush the report.");
            }
        }

        public static void LogInfo(string info)
        {
            _extentTest?.Info(info);
        }

        public static void LogPass(string info)
        {
            _extentTest?.Pass(info);
        }

        public static void LogFail(string info)
        {
            _extentTest?.Fail(info);
        }

        public static void LogScreenshot(string info, string path)
        {
            _extentTest?.Info(info, MediaEntityBuilder.CreateScreenCaptureFromPath(path).Build());
        }

        public void EndTest()
        {
            ActionsInWeb actions = new ActionsInWeb();
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;
            switch (testStatus)
            {
                case TestStatus.Failed:
                    LogFail($"Test has failed: {message}");
                    break;
                case TestStatus.Skipped:
                    LogInfo($"Test Skipped : {message}");
                    break;
                default:
                    break;
            }
            LogScreenshot("End Test", actions.GetScreenshot());
        }

    }
}
