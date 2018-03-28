using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.Core.Model
{
    public interface IStepRunResult
    {
        bool Pass { get; }
        string Message { get; }
        ITestStep Step { get; }
    }
}
