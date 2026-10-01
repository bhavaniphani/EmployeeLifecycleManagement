using EmployeeLifecycleManagement.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Pages
{
    public class PIMPage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelper waitHelper;
        private readonly By pimMenu =By.XPath("//span[normalize-space()='PIM']");
        private readonly By addButton =By.XPath("//button[normalize-space()='Add']");
        private readonly By employeeId =By.XPath("//label[normalize-space()='Employee Id']/following::input[1]");
        private readonly By searchButton =By.XPath("//button[normalize-space()='Search']");

        public PIMPage(IWebDriver driver)
        {
            this.driver = driver;
            waitHelper = new WaitHelper(driver);
        }

        public void OpenPIM()
        {
            waitHelper.WaitForElement(pimMenu).Click();
            waitHelper.WaitForPageToLoad();
        }
        public void ClickAdd()
        {
            waitHelper.WaitForElement(addButton).Click();
            waitHelper.WaitForPageToLoad();
        }
        public void SearchEmployeeById(string id)
        {
            waitHelper.WaitForPageToLoad();

            var field = waitHelper.WaitForElement(employeeId);
            field.Clear();
            field.SendKeys(id);
            waitHelper.WaitForElement(searchButton).Click();
            waitHelper.WaitForPageToLoad();
        }
        public bool IsEmployeeDeleted(string employeeId)
        {
            waitHelper.WaitForPageToLoad();
            By employeeRow = By.XPath($"//div[@role='row']" + $"[.//div[normalize-space()='{employeeId}']]");
            By noRecords = By.XPath("//span[contains(normalize-space(),'No Records Found')]");

            try
            {
                if (driver.FindElements(noRecords).Count > 0)
                    return true;

                return driver.FindElements(employeeRow).Count == 0;
            }
            catch
            {
                return true;
            }
        }

    }
}
