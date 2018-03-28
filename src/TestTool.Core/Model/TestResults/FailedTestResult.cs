using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.Core.Model
{
    public class FailedTestResult : ITestResult
    {
        public FailedTestResult(string message)
        {

        }

        public IEnumerable<IStepRunResult> StepResults => null;

        public bool TestPassed => false;
    }
}
