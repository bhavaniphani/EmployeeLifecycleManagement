using EmployeeLifecycleManagement.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelper waitHelper;
        private readonly By username = By.XPath("//input[@placeholder='Username']");

        private readonly By password =By.XPath("//input[@placeholder='Password']");

        private readonly By loginButton =By.XPath("//button[normalize-space()='Login']");

        public LoginPage(IWebDriver driver)
            
        {
            this.driver = driver;
            waitHelper=new WaitHelper(driver);
        }
        public void Login(string userName, string passWord)
        {
            waitHelper.WaitForElement(username).SendKeys(userName);

            waitHelper.WaitForElement(password).SendKeys(passWord);

            waitHelper.WaitForElement(loginButton).Click();
            waitHelper.WaitForPageToLoad();
        }

    }
}
