using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Utilities
{
    public static class ExtentReportHelper
    {
        private static ExtentReports extent;
        private static string reportPath;
        public static void StartReport()
        {
            reportPath = Path.Combine(
                AppContext.BaseDirectory,
                "Reports",
                "TestReport.html"
            );

            Directory.CreateDirectory(
                Path.GetDirectoryName(reportPath)!
            );

            ExtentSparkReporter sparkReporter =
                new ExtentSparkReporter(reportPath);

            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);
        }

        public static ExtentTest CreateTest(string testName)
        {
            return extent.CreateTest(testName);
        }

        public static void FlushReport()
        {
            extent.Flush();
        }
    }
}

