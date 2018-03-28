using System.Collections.Generic;
using TestTool.Core.Model;

namespace TestTool.Core
{
    public interface ITestResult
    {
        IEnumerable<IStepRunResult> StepResults { get; }
        bool TestPassed { get; }
    }
}
