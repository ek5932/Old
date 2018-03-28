using System;

namespace TestTool.Core.Model
{
    public class ErrorResult : IStepRunResult
    {
        private ITestStep testStep;
        private Exception exception;

        public ErrorResult(ITestStep testStep, Exception exception)
        {
            this.testStep = testStep;
            this.exception = exception;
            this.Pass = false;
        }

        public bool Pass { get; }

        public string Message => throw new NotImplementedException();

        public Exception Exception => throw new NotImplementedException();

        public ITestStep Step => throw new NotImplementedException();
    }
}
