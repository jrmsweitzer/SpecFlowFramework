using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Framework
{
    [Binding]
    public class ScenarioRunner : SpecFlowCommonMethods
    {
        [BeforeFeature()]
        public static void Setup()
        {
            Driver = new FirefoxDriver();
            Driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(1));
            Driver.Manage().Window.Maximize();
            ExcelReader.Init();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
            Driver.Navigate().GoToUrl("data:,");
            Driver.Navigate().Refresh();
            var error = ScenarioContext.Current.TestError;
            if (error != null)
            {
                throw error;
            }
        }

        [AfterFeature()]
        public static void TearDown()
        {
            Driver.Quit();
        }
    }
}
