using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpecFlowFramework.Framework
{
    public class SpecFlowCommonMethods
    {
        public static IWebDriver Driver;
        private IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));

        protected IWebElement Find(By by)
        {
            return Driver.FindElement(by);
        }

        protected ICollection<IWebElement> FindAll(By by)
        {
            return Driver.FindElements(by);
        }

        protected void Click(By by)
        {
            Find(by).Click();
            WaitForPageToLoad();
        }

        protected string GetInnerHtml(By by)
        {
            return Find(by).GetAttribute("innerHTML");
        }

        protected string GetText(By by)
        {
            return Find(by).Text;
        }

        protected bool IsElementPresent(By by)
        {
            if (FindAll(by).Count > 0)
            {
                return true;
            }
            return false;
        }

        protected bool IsElementEnabled(By by)
        {
            if (Find(by).Enabled)
            {
                return true;
            }
            return false;
        }

        protected void Select(By by, string optionText)
        {
            var select = new SelectElement(Find(by));
            if (!select.Equals(null))
            {
                try
                {
                    select.SelectByText(optionText);
                }
                catch
                {
                    var errMsg = String.Format(
                        "PageObjectBase: There is no option '{0}' in {1}.",
                        optionText, by);
                    throw new Exception(errMsg);
                }
            }
            else
            {
                string errMsg = "Cannot find element " + by.ToString();
                throw new NoSuchElementException(errMsg);
            }
        }

        protected void SendKeys(By by, string textToSend)
        {
            Find(by).SendKeys(textToSend);
        }

        protected void WaitForElementToBeDeleted(By by, int timeout = 20000)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (IsElementPresent(by))
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    string errMsg = string.Format(
                        "Element '{0}' was still visible after {1} seconds!",
                        by.ToString(), timeout / 1000);
                    throw new Exception(errMsg);
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }


        protected void WaitForElementToExist(By by, int timeout = 20000)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (!IsElementPresent(by))
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = string.Format(
                        "Could not find element '{0}' after {1} seconds!",
                        by.ToString(), timeout / 1000);
                    throw new ElementNotVisibleException(errMsg);
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }

        protected void WaitForPageToLoad()
        {
            wait.Until(driver => ((IJavaScriptExecutor)Driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

        protected void WaitForUrl(string url, int timeout = 20000)
        {
            var _stopwatch = new Stopwatch();
            _stopwatch.Start();
            while (Driver.Url != url)
            {
                if (_stopwatch.ElapsedMilliseconds > timeout)
                {
                    var errMsg = string.Format("Was not on url '{0}' after {1} seconds!\nCurrent url: {2}",
                        url,
                        timeout / 1000,
                        Driver.Url);

                    throw new Exception(errMsg);
                }
            }
            _stopwatch.Stop();
            _stopwatch.Reset();
        }
    }
}
