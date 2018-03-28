using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestTool.Core
{
    public interface ITestRunner
    {
        Task<ITestResult> Run(ITestPlan test, ITestConfiguration testConfiguration);
    }
}
