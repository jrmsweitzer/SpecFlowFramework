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

        [When(@"I fill out the user form")]
        public void IFillOutTheUserForm()
        {
            By email = ExcelReader.GetByForLabel("Email");
            By firstName = ExcelReader.GetByForLabel("First name");
            By lastName = ExcelReader.GetByForLabel("Last name");
            By address = ExcelReader.GetByForLabel("Address");
            By city = ExcelReader.GetByForLabel("City");
            By state = ExcelReader.GetByForLabel("State");
            By country = ExcelReader.GetByForLabel("Country");
            By phone = ExcelReader.GetByForLabel("Phone");
            By billingRadio = ExcelReader.GetByForLabel("Same As Billing Radio");


            SendKeys(email, "test@email.com");
            SendKeys(firstName, "Test");
            SendKeys(lastName, "User");
            SendKeys(address, "123 Fake Street");
            SendKeys(city, "Fakesville");
            SendKeys(state, "Alabama");
            Select(country, "USA");
            SendKeys(phone, "1234567890");
            Click(billingRadio);
            
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
