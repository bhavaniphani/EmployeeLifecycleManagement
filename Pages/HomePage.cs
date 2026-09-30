using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenQA.Selenium;
using EmployeeLifecycleManagement.Utilities;

namespace EmployeeLifecycleManagement.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelper waitHelper;

        private readonly By userDropdown =
            By.XPath("//span[contains(@class,'oxd-userdropdown-tab')]");

        private readonly By logoutLink =
            By.XPath("//a[normalize-space()='Logout']");

        private readonly By username =
            By.XPath("//input[@placeholder='Username']");

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            waitHelper = new WaitHelper(driver);
        }

        public void Logout()
        {
            // Click user profile/dropdown
            waitHelper.WaitForElement(userDropdown).Click();

            // Click Logout
            waitHelper.WaitForElement(logoutLink).Click();

            // Wait until login page is displayed
            waitHelper.WaitForVisible(username);
        }

        public bool IsLoggedOut()
        {
            return driver.FindElements(username).Count > 0;
        }
    }
}
