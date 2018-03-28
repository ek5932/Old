using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Test.Framework;
using TestTool.Core.Model;

namespace TestTool.Core
{
    public class FileTest
    {
        private readonly ITestRunner testRunner;
        private readonly IEnumerable<IFileParser> fileParsers;

        public async Task<ITestResult> Run(string filePath, ITestConfiguration testConfiguration)
        {
            if (!File.Exists(filePath))
                return new FailedTestResult($"No file with '{filePath}' exists");

            var fileInfo = new FileInfo(filePath);
            IEnumerable<IFileParser> supportedParsers = fileParsers.Where(x => fileInfo.IsOfFormat(x.SupportedFormats));

            if (supportedParsers.Count() == 0)
                return new FailedTestResult($"No file parsers are configured for extension '{fileInfo.Extension}'");

            if (supportedParsers.Count() > 1)
                return new FailedTestResult($"Multiple parsers were returned that satisfied extension '{fileInfo.Extension}': {string.Join(",", fileParsers.Select(x => x.Name))}");

            return await RunTest(filePath, testConfiguration, supportedParsers.First());
        }

        private async Task<ITestResult> RunTest(string filePath, ITestConfiguration testConfiguration, IFileParser fileParser)
        {
            ITestPlan testPlan = null;
            try
            {
                testPlan = fileParser.Parse(filePath);
                if (testPlan == null)
                    throw new ApplicationException($"File parser '{fileParser.Name}' returned Null test plan");
            }
            catch (Exception ex)
            {
                return new FailedTestResult($"Unexpected error parsing file: {ex}");
            }

            return await this.testRunner.Run(testPlan, testConfiguration);
        }
    }
}
