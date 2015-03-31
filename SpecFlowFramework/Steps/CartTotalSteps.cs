using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFramework.Framework;
using System;
using System.Threading;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Steps
{
    [Binding]
    public class CartTotalSteps : ScenarioRunner
    {
        [When(@"I Add To Cart")]
        public void IAddToCart()
        {
            By by = ExcelReader.GetByForLabel("Add To Cart");

            WaitForElementToExist(by);

            Click(by);
        }

        [When(@"I Continue Shopping")]
        public void IContinueShopping()
        {
            By by = ExcelReader.GetByForLabel("Continue Shopping");
            Click(by);

            By notificationBox = ExcelReader.GetByForLabel("Notification Box");
            WaitForElementToBeDeleted(notificationBox);

            Thread.Sleep(3000);
        }

        [When(@"I Go To Checkout")]
        public void IGoToCheckout()
        {
            By by = ExcelReader.GetByForLabel("Go To Checkout");
            Click(by);

            By checkoutTable = ExcelReader.GetByForLabel("Product Checkout Table");
            WaitForElementToExist(checkoutTable);
        }

        [When(@"I continue checkout")]
        public void IContinueCheckout()
        {
            By by = ExcelReader.GetByForLabel("Checkout continue");

            Click(by);
        }

        [Then(@"My cart should have a total of (.*)")]
        public void MyCartShouldHaveATotalOf(int expectedTotal)
        {
            By by = ExcelReader.GetByForLabel("Cart Total");

            var actualTotal = Int32.Parse(Find(by).Text);

            if (actualTotal != expectedTotal)
            {
                actualTotal = Int32.Parse(Find(by).Text);
            }

            Assert.AreEqual(expectedTotal, actualTotal);
        }
    }
}
