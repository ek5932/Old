using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Framework;
using TestTool.Core.Model;

namespace TestTool.Core
{
    public class TestRunner : ITestRunner
    {
        public async Task<ITestResult> Run(ITestPlan testPlan, ITestConfiguration testConfiguration)
        {
            Guard.IsNotNull(testPlan, nameof(testPlan));
            Guard.IsNotNull(testConfiguration, nameof(testConfiguration));

            if (testPlan.Setps.IsNullOrEmpty())
                return new TestResult("Test has no steps");

            return await this.RunTest(testPlan, testConfiguration);
        }

        private async Task<ITestResult> RunTest(ITestPlan testPlan, ITestConfiguration testConfiguration)
        {
            var stepResults = new List<IStepRunResult>();
            var asyncSteps = new List<Task<IStepRunResult>>();

            foreach (var testStep in testPlan.Setps)
            {
                if (stepResults.Any(x => !x.Pass) && testConfiguration.StopOnFirstError)
                    break;

                if (testStep.CanBeRunInParallel)
                {
                    Task<IStepRunResult> taskStep = this.RunStep(testStep);
                    asyncSteps.Add(taskStep);
                    continue;
                }

                if(asyncSteps.Any())
                {
                    IStepRunResult[] collectiveResults = await Task.WhenAll(asyncSteps);
                    stepResults.AddRange(collectiveResults);
                }

                IStepRunResult result = await this.RunStep(testStep);
                stepResults.Add(result);
            }

            return new TestResult(stepResults);
        }

        private async Task<IStepRunResult> RunStep(ITestStep testStep)
        {
            try
            {
                return await testStep.Run(null);
            }
            catch (Exception exception)
            {
                return new ErrorResult(testStep, exception);
            }
        }
    }
}
