using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Framework
{
    [Binding]
    public class BaseTableSteps : ScenarioRunner
    {

        [When(@"I store row number containing variable (.*) from Table (.*) into variable (.*)")]
        public void IStoreRowNumberContainingVariableFromTableIntoVariable(string variableNameToSearch, string locatorLabel, string nameForNewVariable)
        {
            var variableExists = VariableShouldExistInTable(variableNameToSearch, locatorLabel);
            if (!variableExists)
            {
                Assert.Fail(string.Format("{0} does not exist in table {1}", variableNameToSearch, locatorLabel));
            }

            By by = ExcelReader.GetByForLabel(locatorLabel);
            var variableValue = variableDictionary[variableNameToSearch];

            var numRows = by.FindElement(Driver).FindElements(By.XPath("//tbody/tr")).Count;
            var magicRow = -1;

            for (var row = 0; row < numRows; row++)
            {
                var cells = by.FindElement(Driver).FindElements(By.XPath(string.Format("//tbody/tr[{0}]/td", row)));

                foreach (var cell in cells)
                {
                    if (cell.Text.Equals(variableValue))
                    {
                        magicRow = row;
                        break;
                    }
                }
            }

            if (magicRow == -1)
            {
                Assert.Fail(string.Format("{0} does not exist in table {1}", variableNameToSearch, locatorLabel));
            }
            variableDictionary.Add(nameForNewVariable, magicRow.ToString());
        }

        [Then(@"The text in table (.*) in row (.*) column (.*) should be ""(.*)""")]
        public void TheTextInTableInRowColumnShouldBe(string locatorLabel, int row, int column, string expectedString)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var cell = by.FindElement(Driver).FindElement(ByFormatter.XPath("//tr[{0}]/td[{1}]").Format(row, column));

            Assert.AreEqual(expectedString, cell.Text);
        }

        [Then(@"Table (.*) should have (.*) rows")]
        public void TableShouldHaveRows(string locatorLabel, int expectedRowCount)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var actualRowCount = by.FindElement(Driver).FindElements(By.XPath("//tbody/tr")).Count();

            Assert.AreEqual(expectedRowCount, actualRowCount);
        }

        [Then(@"Table (.*) should have (.*) columns")]
        public void TableShouldHaveColumns(string locatorLabel, int expectedColumnCount)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var actualColumnCount = by.FindElement(Driver).FindElements(By.TagName("th")).Count();

            Assert.AreEqual(expectedColumnCount, actualColumnCount);
        }

        [Then(@"Variable (.*) should exist in Table (.*)")]
        public bool VariableShouldExistInTable(string variableName, string locatorLabel)
        {
            By by = ExcelReader.GetByForLabel(locatorLabel);

            var variableValue = variableDictionary[variableName];
            if (string.IsNullOrEmpty(variableValue))
            {
                Assert.Fail(string.Format("Variable {0} has not been saved!"));
            }

            var textExists = false;
            var cells = by.FindElement(Driver).FindElements(By.XPath("//tbody//td"));

            foreach (var cell in cells)
            {
                if (cell.Text.Equals(variableValue))
                {
                    textExists = true;
                    break;
                }
            }

            Assert.IsTrue(textExists);
            return textExists;
        }

        [Then(@"The text of Table (.*) Row (.*) Column (.*) should be (.*)")]
        public void TheTextOfTableRowColumnShouldBe(string tableLocatorLabel, string row, string column, string expectedText)
        {
            By by = ExcelReader.GetByForLabel(tableLocatorLabel);

            int rowInt = -1;
            int columnInt = -1;

            //STORE INTO BOOL
            Int32.TryParse(row, out rowInt);
            Int32.TryParse(column, out columnInt);

            if (rowInt == 0)
            {
                row = variableDictionary[row];
            }
            if (columnInt == 0)
            {
                column = variableDictionary[column];
            }

            if (variableDictionary.ContainsKey(expectedText))
            {
                expectedText = variableDictionary[expectedText];
            }

            var actualText = by.FindElement(Driver).FindElement(By.XPath(string.Format("//tbody/tr[{0}]/td[{1}]", row, column))).Text;

            Assert.AreEqual(expectedText, actualText);
        }
    }
}
