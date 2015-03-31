using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFramework.Framework;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Steps
{
    [Binding]
    public class HerokuLoginSteps : ScenarioRunner
    {
        [When(@"I try to go directly to the secure area")]
        public void WhenITryToGoDirectlyToTheSecureArea()
        {
            Driver.Navigate()
                .GoToUrl("http://the-internet.herokuapp.com/secure");
        }

        [Then(@"I should log in successfully")]
        public void ThenIShouldLogInSuccessfully()
        {
            var expectedURL = "http://the-internet.herokuapp.com/secure";
            var actualURL = Driver.Url;

            Assert.AreEqual(expectedURL, actualURL);
        }

        [Then(@"I should not log in successfully")]
        public void ThenIShouldNotLogInSuccessfully()
        {
            var expectedURL = "http://the-internet.herokuapp.com/login";
            var actualURL = Driver.Url;

            Assert.AreEqual(expectedURL, actualURL);
        }

        [Then(@"There should be an error message on the screen")]
        public void ThenThereShouldBeAnErrorMessageOnTheScreen()
        {
            By by = ExcelReader.GetByForLabel("Login Error Message");

            Assert.IsTrue(IsElementPresent(by));
        }

        [Then(@"The error message should say ""(.*)""")]
        public void ThenTheErrorMessageShouldSay(string errorMessage)
        {
            By by = ExcelReader.GetByForLabel("Login Error Message");
            string actualErrorMessage = GetText(by);

            Assert.IsTrue(actualErrorMessage.Contains(errorMessage));
        }
    }
}
