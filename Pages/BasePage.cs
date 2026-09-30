using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLifecycleManagement.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;
        protected readonly WebDriverWait wait;

        private readonly By formLoader =
            By.CssSelector(".oxd-form-loader");

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;

            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(20)
            );
        }
        protected void WaitForPageToLoad()
        {
            wait.Until(d =>
            {
                var loaders = d.FindElements(formLoader);

                return loaders.Count == 0 ||
                       loaders.All(x => !x.Displayed);
            });
        }

        protected IWebElement WaitForElement(By locator)
        {
            return wait.Until(d =>
            {
                try
                {
                    var element = d.FindElement(locator);

                    return element.Displayed && element.Enabled
                        ? element
                        : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });


        }

    }
}
