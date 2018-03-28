using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.Core.Model
{
    public class TestResult : ITestResult
    {
        private List<IStepRunResult> stepResults;

        public TestResult(string message = null)
        {

        }

        public TestResult(List<IStepRunResult> stepResults)
        {
            this.stepResults = stepResults;
        }

        public IEnumerable<IStepRunResult> StepResults => throw new NotImplementedException();

        public bool TestPassed => throw new NotImplementedException();
    }
}
