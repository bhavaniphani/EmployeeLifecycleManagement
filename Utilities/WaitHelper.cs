using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Linq;

namespace EmployeeLifecycleManagement.Utilities
{
    public class WaitHelper
    {
        private readonly WebDriverWait wait;

        public WaitHelper(IWebDriver driver, int timeoutInSeconds = 50)
        {
            wait = new WebDriverWait(
                driver,
                TimeSpan.FromSeconds(timeoutInSeconds)
            );
        }

        // Wait until element is visible and enabled
        public IWebElement WaitForElement(By locator)
        {
            return wait.Until(d =>
            {
                try
                {
                    IWebElement element = d.FindElement(locator);

                    return element.Displayed && element.Enabled
                        ? element
                        : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }

        // Wait until element is visible
        public IWebElement WaitForVisible(By locator)
        {
            return wait.Until(d =>
            {
                try
                {
                    IWebElement element = d.FindElement(locator);

                    return element.Displayed
                        ? element
                        : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }

        // Wait until element is clickable
        public IWebElement WaitForClickable(By locator)
        {
            return wait.Until(d =>
            {
                try
                {
                    IWebElement element = d.FindElement(locator);

                    return element.Displayed && element.Enabled
                        ? element
                        : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
        }

        // Wait until element disappears
        public void WaitForElementToDisappear(By locator)
        {
            wait.Until(d =>
            {
                try
                {
                    var elements = d.FindElements(locator);

                    return elements.Count == 0 ||
                           elements.All(x => !x.Displayed);
                }
                catch (StaleElementReferenceException)
                {
                    return true;
                }
            });
        }

        // Wait for OrangeHRM loader to disappear
        public void WaitForPageToLoad()
        {
            By formLoader =
                By.CssSelector(".oxd-form-loader");

            WaitForElementToDisappear(formLoader);
        }

        // Wait until text is present
        public void WaitForText(
            By locator,
            string expectedText)
        {
            wait.Until(d =>
            {
                try
                {
                    IWebElement element =
                        d.FindElement(locator);

                    return element.Displayed &&
                           element.Text.Contains(expectedText);
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }

        // Wait until element is displayed
        public bool WaitForElementDisplayed(By locator)
        {
            return wait.Until(d =>
            {
                try
                {
                    IWebElement element =
                        d.FindElement(locator);

                    return element.Displayed;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
                catch (StaleElementReferenceException)
                {
                    return false;
                }
            });
        }
    }
}