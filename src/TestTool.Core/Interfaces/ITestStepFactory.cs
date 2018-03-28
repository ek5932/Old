namespace TestTool.Core
{
    public interface ITestStepFactory
    {
        ITestStep CreateComparisonStep(string action, IExpectedContent expectedContent);
    }
}
