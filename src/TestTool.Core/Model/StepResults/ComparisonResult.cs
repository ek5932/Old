using System;
using Test.Framework;

namespace TestTool.Core.Model.Results
{
    public class ComparisonResult : IStepRunResult
    {
        private readonly Lazy<bool> lazyResult;

        public ComparisonResult(ITestStep testStep, IExpectedContent expectedContent, IActualContent actualContent)
        {
            Guard.IsNotNull(testStep, nameof(testStep));
            Guard.IsNotNull(expectedContent, nameof(expectedContent));
            Guard.IsNotNull(actualContent, nameof(actualContent));

            this.Step = testStep;
            lazyResult = new Lazy<bool>(() => expectedContent.CompareTo(actualContent));
        }

        public ITestStep Step { get; }

        public bool Pass => lazyResult.Value;

        public string Message => throw new NotImplementedException();
    }
}
