using System;
using System.Collections.Generic;
using System.Linq;
using Test.Framework;
using TestTool.Core;
using TestTool.Core.Enumerations;
using TestTool.Core.Factories;

namespace TestTool.Parser.Excel
{
    internal class ExcelStep
    {
        private IExpectedContent expectedContent;
        private readonly TestAction testAction;
        private readonly List<IEnumerable<string>> stepContent;
        private readonly IExpectedContentFactory expectedContentFactory;

        private string[] actionChars = new[] { "$", "!" };

        public ExcelStep(TestAction testAction, List<IEnumerable<string>> stepContent)
        {
            this.testAction = testAction;
            this.stepContent = stepContent;
        }

        private void ParseStep()
        {
            if (this.stepContent.IsNullOrEmpty())
                throw new ApplicationException("Step contains no content");

            int startingRowIndex = this.GetActionRowIndex();
            int actionRowCount = this.stepContent.Count - startingRowIndex;
            string actionString = this.GetRowString(startingRowIndex);

            if (actionRowCount == 1)
            {
                // Simple in-line assert of a scalar value
                this.ParseInlineAssert(actionString);
            }

            if (this.stepContent.Count == 2)
            {
                // Scalar value assert over multiple lines
                var value = this.ExtractAssertValue(actionString);
                if (value.IsNullOrEmpty())
                {
                    var singleLineActionString = $"{actionString} {string.Join(" ", this.GetRowString(1))}";
                    this.ParseInlineAssert(actionString);
                }




                if (actionString.Contains(":"))
                {
                    string assertValue = this.ExtractAssertValue(actionString);

                }

                // 
                else
                {

                }

            }




            if (this.stepContent.Count == 1 || this.stepContent.Count == 2)
            {
                // 
                var singleLineActionString = actionString;
                if (this.stepContent.Count == 2)
                    singleLineActionString += $" {string.Join(" ", this.GetRowString(1))}";

                this.ParseInlineAssert(singleLineActionString);
            }

            if (this.stepContent.Count == 1)
            {
                this.ParseInlineAssert(rowAction, actionString);
            }
        }

        private int GetActionRowIndex()
        {
            for (int currentIndex = 0; currentIndex < stepContent.Count; currentIndex++)
            {
                string rowString = string.Join(" ", this.stepContent[currentIndex]);
                if (this.actionChars.Any(x => rowString.Contains(x)))
                    return currentIndex;
            }

            return -1;
        }

        private string GetRowString(int index)
        {
            var rowContent = this.stepContent.ElementAt(index).Where(x => !x.IsNullOrEmpty());
            var rowString = string.Join(" ", rowContent);

            foreach (var actionChar in actionChars)
            {
                // Move to the current action
                if (rowString.Contains(actionChar))
                    rowString = rowString.Substring(rowString.IndexOf(actionChar));
            }

            return rowString;
        }

        private void ParseInlineAssert(string actionString)
        {
            string assertValue = this.ExtractAssertValue(actionString);
            if (assertValue.IsNullOrEmpty())
                throw new ApplicationException("Unable to extract in-line assert value");

            this.expectedContent = this.expectedContentFactory.CreateExpectedScalarContent(assertValue);
        }

        private string ExtractAssertValue(string actionString)
        {
            string assertValue = null;
            if (actionString.Contains(":"))
            {
                assertValue = actionString.Substring(actionString.IndexOf(":") + 1).Trim();
            }

            return assertValue;
        }
    }
}
