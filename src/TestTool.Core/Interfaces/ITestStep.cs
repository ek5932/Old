using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestTool.Core.Model;

namespace TestTool.Core
{
    public interface ITestStep
    {
        bool CanBeRunInParallel { get; }
        string Name { get; }

        Task<IStepRunResult> Run(ITestContext testContext);
    }
}
