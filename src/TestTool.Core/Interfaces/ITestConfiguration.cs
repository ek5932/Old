using System;
using System.Collections.Generic;
using System.Text;

namespace TestTool.Core
{
    public interface ITestConfiguration
    {
        bool StopOnFirstError { get; }
    }
}
