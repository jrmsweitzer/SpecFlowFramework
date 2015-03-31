using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Framework
{
    [Binding]
    public class BaseSteps : ScenarioRunner
    {
        [Given(@"I am at url ""(.*)""")]
        public void IAmAtUrl(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        [When(@"I input ""(.*)"" into (.*)")]
        public void IInputInto(string input, string locatorLabel)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            Find(by).SendKeys(input);
        }

        [When(@"I select option ""(.*)"" from (.*)")]
        public void ISelectOptionFrom(string optionText, string locatorLabel)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

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

        [When(@"I click link with exact text ""(.*)""")]
        public void IClickLinkWithExactText(string linkText)
        {
            By by = By.XPath(string.Format("//a[.='{0}']", linkText));

            WaitForElementToExist(by);

            Click(by);
        }

        [When(@"I click link containing text ""(.*)""")]
        public void IClickLinkContainingText(string linkText)
        {
            By by = By.XPath(string.Format("//a[contains(.,'{0}')]", linkText));

            Click(by);
        }

        [When(@"I click on (.*)")]
        public void IClickOn(string locatorLabel)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            Click(by);
        }

        [When(@"I store text of (.*) into variable (.*)")]
        public void IStoreTextOfIntoVariable(string locatorLabel, string variableName)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var text = GetText(by);

            variableDictionary.Add(variableName, text);
        }

        [Then(@"My url should match ""(.*)""")]
        public void MyUrlShouldMatch(string expectedUrl)
        {
            var actualUrl = Driver.Url;

            Assert.IsTrue(actualUrl.Contains(expectedUrl));
        }

        [Then(@"(.*) should have ""(.*)"" selected")]
        public void ShouldHaveSelected(string locatorLabel, string optionText)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var select = new SelectElement(Find(by));
            Assert.AreEqual(optionText, select.SelectedOption.Text);
        }

        [Then(@"(.*) should be (.*)checked")]
        public void ShouldBe_Checked(string locatorLabel, string un)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);


            if (string.IsNullOrEmpty(un))
            {
                Assert.IsTrue(Find(by).Selected);
            }
            else if (un.Equals("un"))
            {
                Assert.IsFalse(Find(by).Selected);
            }
        }

        [Then(@"I should have no broken images")]
        public void IShouldHaveNoBrokenImages()
        {
            ICollection<IWebElement> allImages = Driver.FindElements(By.TagName("img"));
            foreach (var image in allImages)
            {
                bool loaded = (bool)((IJavaScriptExecutor)Driver).ExecuteScript(
                    "return arguments[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && arguments[0].naturalWidth > 0", image);
                if (!loaded)
                {
                    Assert.Fail("There are broken images!");
                }
            }
        }

        [Then(@"Variable (.*) should be ""(.*)""")]
        public void VariableShouldBe(string variableName, string expectedValue)
        {
            if (!variableDictionary.ContainsKey(variableName))
            {
                Assert.Fail("Variable {0} does not exist!");
            }

            var actualValue = variableDictionary[variableName];

            Assert.AreEqual(expectedValue, actualValue);
        }

        [Then(@"Text of (.*) should be (.*)")]
        public void TextOfShouldBe(string locatorLabel, string expectedValue)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            if (variableDictionary.ContainsKey(expectedValue))
            {
                expectedValue = variableDictionary[expectedValue];
            }

            var actualValue = Find(by).Text;

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
