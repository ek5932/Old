using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ExcelDataReader;
using Test.Framework;
using TestTool.Core;
using TestTool.Core.Enumerations;

namespace TestTool.Parser.Excel
{
    public class ExcelFileParser : IFileParser
    {
        private readonly ITestStepFactory testStepFactory;

        public ExcelFileParser()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public IEnumerable<string> SupportedFormats => new[] { "XLS", "XLSX", "CSV" };

        public string Name => nameof(ExcelFileParser);

        public ITestPlan Parse(string filePath)
        {
            Guard.IsNotNullOrEmpty(filePath, nameof(filePath));

            if (!File.Exists(filePath))
                throw new ApplicationException($"File '{filePath}' does not exist");

            if (this.FileIsValidFormat(filePath))
                throw new ApplicationException($"File '{filePath}' is not supproted");

            return this.ParseFile(filePath);
        }

        private bool FileIsValidFormat(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return fileInfo.IsOfFormat(this.SupportedFormats);
        }

        private ITestPlan ParseFile(string filePath)
        {
            List<List<string>> tableRows = ExtractTableData(filePath);
            IEnumerable<ITestStep> testSteps = this.ExtractSteps(tableRows);
            return new TestPlan(testSteps);
        }

        private List<List<string>> ExtractTableData(string filePath)
        {
            var tableRows = new List<List<string>>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                while (reader.Read())
                {
                    var rowValues = new List<string>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        object value = reader.GetValue(i) ?? string.Empty;
                        rowValues.Add(value.ToString());
                    }

                    tableRows.Add(rowValues);
                }
            }

            return tableRows;
        }

        private IEnumerable<ITestStep> ExtractSteps(List<List<string>> tableRows)
        {
            var steps = new List<ExcelStep>();

            TestAction lastAction = TestAction.Unknown;
            for (int i = 0; i < tableRows.Count; i++)
            {
                List<string> row = tableRows[i];
                IEnumerable<string> rowContent = row.Where(x => !x.IsNullOrEmpty());
                if (rowContent.Any())
                {
                    TestAction currentAction;
                    var firstValue = rowContent.First().Trim();
                    if (Enum.TryParse(firstValue, out currentAction))
                        lastAction = currentAction;

                    if (rowContent.Any(x => ContainsStep(x)))
                    {
                        // Extract the current step and move the index to the next
                        var stepContents = GetCurrentStepContent(tableRows, i, out i);
                        if (!stepContents.IsNullOrEmpty())
                            steps.Add(new ExcelStep(lastAction, stepContents));
                    }
                }
            }

            return this.CreateTestSteps(steps);
        }

        private IEnumerable<ITestStep> CreateTestSteps(List<ExcelStep> steps)
        {
            throw new NotImplementedException();
        }

        private List<IEnumerable<string>> GetCurrentStepContent(List<List<string>> tableRows, int currentIndex, out int newIndex)
        {
            newIndex = currentIndex;
            var stepContent = new List<IEnumerable<string>>();
            for (; currentIndex < tableRows.Count; currentIndex++)
            {
                List<string> row = tableRows[currentIndex];
                IEnumerable<string> rowContent = row.Where(x => !x.IsNullOrEmpty());
                if (!rowContent.Any())
                {
                    newIndex = currentIndex;
                    break;
                }

                stepContent.Add(row);
            }

            return stepContent;
        }

        private bool ContainsStep(string rowContent)
        {
            return rowContent.Contains("!") || rowContent.Contains("$");
        }
    }
}
