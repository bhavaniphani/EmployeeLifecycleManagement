using EmployeeLifecycleManagement.Models;
using EmployeeLifecycleManagement.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeLifecycleManagement.Pages
{
    public class EmployeePage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelper waitHelper;
        
        private readonly By firstName = By.XPath("//input[@placeholder='First Name']");
        private readonly By lastName = By.XPath("//input[@placeholder='Last Name']");
        private readonly By imgInput = By.XPath("//input[@type='file' and @class='oxd-file-input']");
        private readonly By empId = By.XPath("//label[contains(normalize-space(),'Employee Id')]/following::input[1]");
        private readonly By empsaveButton = By.XPath("//button[normalize-space()='Save']");
        private readonly By employeeNameInput = By.XPath("//label[normalize-space()='Employee Name']/following::input[1]");
        private readonly By employeeIdSearch =By.XPath("//label[normalize-space()='Employee Id']/following::input[1]");
        private readonly By searchButton = By.XPath("//button[normalize-space()='Search']");
        private readonly By employmentStatusDropdown = By.XPath("//label[contains(normalize-space(),'Employment Status')]/following::div[1]");
        private readonly By jobTitleDropdown = By.XPath("//label[contains(normalize-space(),'Job Title')]/following::div[1]");
        private readonly By successMessage = By.XPath("//div[contains(@class,'oxd-toast')]");
        private readonly By clickJobTab = By.XPath("//a[normalize-space()='Job']");
        private readonly By jobsaveButton = By.XPath("//button[normalize-space()='Save']");
        private readonly By pageLoader = By.XPath("//div[contains(@class,'oxd-loading-spinner')]");
        private readonly By pimMenu = By.XPath("//a[.//span[normalize-space()='PIM']]");
        private readonly By formLoader = By.CssSelector(".oxd-form-loader");
        private readonly By saveButton =By.XPath("//button[normalize-space()='Save']");

        public EmployeePage(IWebDriver driver)
        {
            this.driver = driver;
            waitHelper = new WaitHelper(driver);
        }

        public void CreateEmployee(Employee employee)
        {

            Console.WriteLine($"Creating employee: {employee.FirstName}");
            Console.WriteLine($"Employee ID: {employee.EmployeeId}");
            IWebElement element = waitHelper.WaitForElement(firstName);
            element.SendKeys(employee.FirstName);
            Console.WriteLine($"Driver is null: {driver == null}");
            driver.FindElement(lastName).SendKeys(employee.LastName);
            IWebElement empidTextbox = driver.FindElement(empId);
            // Select existing text
            empidTextbox.SendKeys(Keys.Control + "a");
            // Delete existing text
            empidTextbox.SendKeys(Keys.Backspace);
            empidTextbox.SendKeys(employee.EmployeeId);
            driver.FindElement(imgInput).SendKeys(employee.Profilepic);
            waitHelper.WaitForPageToLoad();
            driver.FindElement(empsaveButton).Click();
            waitHelper.WaitForPageToLoad();
            bool saved = waitHelper.WaitForElementDisplayed(successMessage);
            Assert.That(saved, Is.True, "Employee was not saved successfully.");
            Console.WriteLine("Employee saved successfully.");
        }

        public void ClickJob()
        {
            waitHelper.WaitForPageToLoad();
            IWebElement job = waitHelper.WaitForElement(clickJobTab);
            job.Click();
        }
       
        public void SelectEmploymentStatus(string status)
        { 
            driver.FindElement(employmentStatusDropdown).Click();
            By empstatusOption = By.XPath($"//div[@role='option']//span[normalize-space()='{status}']");
            waitHelper.WaitForElement(empstatusOption).Click();
           
        }
        //select job title
        public void SelectJobTitle(string jobTitle)
        {
            waitHelper.WaitForPageToLoad();
            IWebElement jTitle = waitHelper.WaitForVisible(jobTitleDropdown);
            //waitHelper.WaitForPageToLoad();
            jTitle.Click();
            By jobTitleoption = By.XPath($"//div[@role='option']//span[normalize-space()='{jobTitle}']");
            waitHelper.WaitForElement(jobTitleoption).Click();
        }
        public void ClickSave()
        {
            driver.FindElement(jobsaveButton).Click();
        }
        public void OpenEmployee(string employeeId)
        {
            // Wait until OrangeHRM loader disappears
            waitHelper.WaitForPageToLoad();

            // Find the employee row using Employee ID
            By employeeRow = By.XPath($"//div[@role='row'][.//div[normalize-space()='{employeeId}']]");

            // Wait until employee row is visible
            IWebElement row = waitHelper.WaitForVisible(employeeRow);

            // Scroll the employee row into view
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});",row);

            // Find Edit button inside the employee row
            IWebElement editButton = row.FindElement(By.XPath(".//button[.//i[contains(@class,'bi-pencil-fill')]]"));

            // Click Edit
            editButton.Click();

            // Wait for the employee page to load
            waitHelper.WaitForPageToLoad();
        }
        public string GetJobTitle()
        {
            waitHelper.WaitForPageToLoad();
            IWebElement dropdown = waitHelper.WaitForVisible(jobTitleDropdown);
            return dropdown.Text.Trim();
        }
        public string GetEmploymentStatus()
        {
            waitHelper.WaitForPageToLoad();
            IWebElement dropdown =waitHelper.WaitForVisible(employmentStatusDropdown);
            return dropdown.Text.Trim();
        }
        public void SaveChanges()
        {
            waitHelper.WaitForPageToLoad();
            IWebElement save = waitHelper.WaitForElement(saveButton);
            save.Click();

            // Wait for save operation to complete
            waitHelper.WaitForPageToLoad();
        }
        public void UpdateJobTitleAndEmploymentStatus(string jobTitle, string employmentStatus)
        {

            // Wait until page loader disappears
            waitHelper.WaitForPageToLoad();
            IWebElement dropdown = waitHelper.WaitForVisible(jobTitleDropdown);
            // Job Title
            IWebElement jobTitleElement = driver.FindElement(jobTitleDropdown);
            waitHelper.WaitForPageToLoad();
            jobTitleElement.Click();
            By jobTitleOption = By.XPath($"//div[@role='option'][normalize-space()='{jobTitle}']");
            waitHelper.WaitForElement(jobTitleOption).Click();
            Console.WriteLine("jobTitleOption saved successfully.");
            // Employment Status
            IWebElement employmentStatusElement = driver.FindElement(employmentStatusDropdown);
            employmentStatusElement.Click();
            By employmentStatusOption = By.XPath($"//div[@role='option'][normalize-space()='{employmentStatus}']");
            waitHelper.WaitForElement(employmentStatusOption).Click();
            Console.WriteLine("employmentStatusOption saved successfully.");
            // Save
            By saveButton = By.XPath("//button[normalize-space()='Save']");

            waitHelper.WaitForElement(saveButton).Click();
            Console.WriteLine("save button clicked successfully.");
            // Wait for update to complete
            waitHelper.WaitForPageToLoad();
        }
        public void DeleteEmployee(string employeeId)
        {
            waitHelper.WaitForPageToLoad();
            By employeeRow = By.XPath($"//div[@role='row']" + $"[.//div[normalize-space()='{employeeId}']]");
            IWebElement row =waitHelper.WaitForVisible(employeeRow);
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView({block:'center'});",row);
            // Find Delete button inside the employee row
            IWebElement deleteButton =row.FindElement(By.XPath(".//button[.//i[contains(@class,'bi-trash')]]"));
            deleteButton.Click();
            waitHelper.WaitForPageToLoad();
            // Confirm deletion popup
            By confirmDeleteButton =By.XPath("//button[normalize-space()='Yes, Delete']");
            waitHelper.WaitForElement(confirmDeleteButton).Click();
            Console.WriteLine("Delete button clicked successfully.");
            waitHelper.WaitForPageToLoad();
        }

        
    }

}

