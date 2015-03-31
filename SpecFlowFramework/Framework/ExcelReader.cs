using Excel;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.IO;

namespace SpecFlowFramework.Framework
{
    public class ExcelReader
    {
        private static Dictionary<string, Locator> Locators;

        public static By GetByForLabel(string locatorLabel)
        {
            if (!Locators.ContainsKey(locatorLabel.ToLower()))
            {
                Assert.Fail("Label {0} not defined!", locatorLabel);
            }

            var locator = Locators[locatorLabel.ToLower()];

            switch (locator.LocatorType.ToLower())
            {
                case "id":
                    return By.Id(locator.LocatorString);
                case "class":
                case "classname":
                case "class_name":
                    return By.ClassName(locator.LocatorString);
                case "tagname":
                    return By.TagName(locator.LocatorString);
                case "xpath":
                    return By.XPath(locator.LocatorString);
            }

            return null;
        }

        public static void Init()
        {
            Locators = new Dictionary<string, Locator>();

            var workbookDirectory = @"c:\users\jswei_000\documents\visual studio 2013\Projects\SpecFlowFramework\SpecFlowFramework\Locators\";
            string[] filePaths = Directory.GetFiles(workbookDirectory);

            foreach (var file in filePaths)
            {
                string filename = file.ToString();

                foreach (var worksheet in Workbook.Worksheets(file))
                {
                    foreach (var row in worksheet.Rows)
                    {
                        int cellIndex;

                        string label = "";
                        string locatorType = "";
                        string locator = "";

                        for (cellIndex = 0; cellIndex < row.Cells.Length; cellIndex++)
                        {
                            if (row.Cells[cellIndex] != null)
                            {
                                if (cellIndex == 0)
                                {
                                    label = row.Cells[cellIndex].Text;
                                }
                                if (cellIndex == 1)
                                {
                                    locatorType = row.Cells[cellIndex].Text;
                                }
                                if (cellIndex == 2)
                                {
                                    locator = row.Cells[cellIndex].Text;
                                }
                            }
                        }

                        Locator loc = new Locator();
                        loc.LocatorType = locatorType;
                        loc.LocatorString = locator;

                        Locators.Add(label.ToLower(), loc);
                    }
                }
            }
        }
    }
}
