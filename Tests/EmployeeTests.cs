using EmployeeLifecycleManagement.Models;
using EmployeeLifecycleManagement.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Text.Json;
using SeleniumExtras.WaitHelpers;
using EmployeeLifecycleManagement.Utilities;

namespace EmployeeLifecycleManagement.Tests
{
    public class EmployeeTests : BaseTest
    {
        private List<Employee> employees;

        [SetUp]
        public void LoadTestData()
        {
            employees = JsonReader.ReadJson<Employee>(
                "employees.json"
            );
        }

        [Test]
        public void CreateEmployee()
        {
            
            
                Employee employee = employees[0];
               
                
                pimPage.OpenPIM();
                
                pimPage.ClickAdd();
               
                employeePage.CreateEmployee(employee);
                

                
            employeePage.ClickJob();

            employeePage.SelectJobTitle(employee.JobTitle);

            employeePage.SelectEmploymentStatus(employee.EmploymentStatus);

            employeePage.SaveChanges();
        }
        [Test]
        public void EditEmployeeInformation()
        {
            Employee employee = employees[0];
            string newJobTitle = "Automaton Tester";
            string newEmploymentStatus = "Freelance";
            pimPage.OpenPIM();
            pimPage.SearchEmployeeById(employee.EmployeeId);
            employeePage.OpenEmployee(employee.EmployeeId);
            employeePage.ClickJob();
            employeePage.UpdateJobTitleAndEmploymentStatus(newJobTitle, newEmploymentStatus);
            string actualJobTitle = employeePage.GetJobTitle();
            Assert.That(actualJobTitle, Is.EqualTo(newJobTitle), "Job Title was not updated successfully.");
            string actualEmploymentStatus = employeePage.GetEmploymentStatus();
            Assert.That(actualEmploymentStatus, Is.EqualTo(newEmploymentStatus), "Employment Status was not updated successfully.");

        }
        [Test]
        public void DeleteEmployeeAndVerifyDeletion()
        {
            var test = ExtentReportHelper.CreateTest("Delete Employee And Verify Deletion");
            test.Info("Opening PIM page");

            Employee employee = employees[0];

            test.Info("Opening PIM page");
            pimPage.OpenPIM();

            test.Info("Searching employee");
            pimPage.SearchEmployeeById(employee.EmployeeId);

            test.Info("Deleting employee");
            employeePage.DeleteEmployee(employee.EmployeeId);

            test.Info("Verifying employee deletion");
            pimPage.SearchEmployeeById(employee.EmployeeId);

            
            bool isDeleted = pimPage.IsEmployeeDeleted(employee.EmployeeId);

            Assert.That(
                isDeleted,
                Is.True,
                $"Employee with ID {employee.EmployeeId} was not deleted."
            );
            test.Pass("Employee deleted successfully");
        }
        [Test]
        public void LogoutAndVerifySessionInvalidated()
        {
            homePage.Logout();
            //bool isLoggedOut = homePage.IsLoggedOut();
            Assert.That(driver.Url, Does.Contain("/auth/login"), "User was not redirected to login page after logout.");
            // Try to access a protected page
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com/web/index.php/pim/viewEmployeeList");
            waitHelper.WaitForVisible(By.XPath("//input[@placeholder='Username']"));
            Assert.That(driver.Url, Does.Contain("/auth/login"), "Protected page was accessible after logout.");
        }

    }
}