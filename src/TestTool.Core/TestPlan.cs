using System;
using System.Collections.Generic;
using System.Text;
using Test.Framework;

namespace TestTool.Core
{
    public class TestPlan : ITestPlan
    {
        public TestPlan(IEnumerable<ITestStep> step)
        {
            Guard.IsNotNullOrEmpty(step, nameof(step));
        }

        public IEnumerable<ITestStep> Setps { get; }
    }
}
