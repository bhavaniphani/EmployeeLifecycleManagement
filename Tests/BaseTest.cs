using EmployeeLifecycleManagement.Pages;
using EmployeeLifecycleManagement.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected LoginPage loginPage;
        protected PIMPage pimPage;
        protected EmployeePage employeePage;
        protected HomePage homePage;
        protected WaitHelper waitHelper;
        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            waitHelper = new WaitHelper(driver);
            driver.Manage().Window.Maximize();

            driver.Navigate().GoToUrl(
                "https://opensource-demo.orangehrmlive.com/"
            );
            loginPage = new LoginPage(driver);
            pimPage = new PIMPage(driver);
            employeePage = new EmployeePage(driver);
            homePage = new HomePage(driver);

            loginPage.Login("Admin", "admin123");
        }


        [TearDown]
        public void TearDown()
        {
            //ExtentReportHelper.FlushReport();
            
                driver.Quit();
            
        }
    }
}
